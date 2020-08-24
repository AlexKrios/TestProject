using Battle;
using System;
using System.Collections;
using Units.Objects.BattleUnit;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Animations
{
    public class UnitAnimation : MonoBehaviour, IAnimTurnStart, IAnimTurnEnd, IAnimAttack, IAnimHit
    {
        private Manager Manager { get => Manager.Instance; }
        private BattleUnitObject CurrentUnit { get => Manager.currentUnit; }
        private BattleUnitObject TargetUnit { get => Manager.targetUnit; }
        [NonSerialized] public UnityEvent OnAttackTarget = new UnityEvent();

        private void Start()
        {
            OnAttackTarget.AddListener(() => StartCoroutine(RotateToTarget()));
        }

        public void TurnStart()
        {
            var animator = gameObject.GetComponent<Animator>();
            animator.SetBool("isTurn", true);
        }

        public void TurnEnd()
        {
            var animator = gameObject.GetComponent<Animator>();
            animator.SetBool("isTurn", false);
        }

        public void Attack()
        {
            var animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("isAttack");
        }

        public void HitOrDead()
        {
            var animator = gameObject.GetComponent<Animator>();          

            if (TargetUnit.Unit.CurrentHp <= 0)
            {
                animator.SetBool("isDead", true);
                return;
            }
            
            animator.Play("Hit", 0, 0.0f);
        }

        private IEnumerator RotateToTarget()
        {
            CurrentUnit.UnitGO.GetComponent<IAnimAttack>().Attack();

            /* Object transform */
            var currentUnitParent = CurrentUnit.Parent.transform;
            var targetUnitParent = TargetUnit.Parent.transform;
            /* Target rotation */
            var targetR = Quaternion.LookRotation(targetUnitParent.position - currentUnitParent.position);

            for (float timer = 0f; timer < .5f; timer += Time.deltaTime)
            {
                currentUnitParent.rotation = Quaternion.Slerp(currentUnitParent.rotation, targetR, 2 * timer);
                yield return null;
            }

            currentUnitParent.rotation = targetR;
        }

        public IEnumerator RotateToDefault()
        {
            Manager.RemoveBattleStatus("UnitAttack");

            CurrentUnit.UnitGO.GetComponent<IAnimTurnEnd>().TurnEnd();

            var currentUnitParent = CurrentUnit.Parent.transform;

            for (float timer = 0f; timer < .5f; timer += Time.deltaTime)
            {
                currentUnitParent.localRotation = Quaternion.Slerp(currentUnitParent.localRotation, Quaternion.identity, 2 * timer);
                yield return null;
            }

            currentUnitParent.localRotation = Quaternion.identity;
        }
    }
}