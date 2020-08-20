using Battle;
using Battle.Units;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Animations
{
    public class UnitAnimation : MonoBehaviour, IAnimTurnStart, IAnimTurnEnd, IAnimAttack, IAnimHit
    {
        private Manager Manager { get => Manager.Instance; }
        private UnitStatus CurrentUnit { get => Manager.currentUnit; }
        private UnitStatus TargetUnit { get => Manager.targetUnit; }
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

            if (TargetUnit.currentHp <= 0)
            {
                animator.SetBool("isDead", true);
                return;
            }
            
            animator.Play("Hit", 0, 0.0f);
        }

        private IEnumerator RotateToTarget()
        {
            CurrentUnit.gameObject.GetComponent<IAnimAttack>().Attack();

            /* Object transform */
            var currentUnitParent = CurrentUnit.parent.transform;
            var targetUnitParent = TargetUnit.parent.transform;
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

            CurrentUnit.gameObject.GetComponent<IAnimTurnEnd>().TurnEnd();

            var currentUnitParent = CurrentUnit.parent.transform;

            for (float timer = 0f; timer < .5f; timer += Time.deltaTime)
            {
                currentUnitParent.localRotation = Quaternion.Slerp(currentUnitParent.localRotation, Quaternion.identity, 2 * timer);
                yield return null;
            }

            currentUnitParent.localRotation = Quaternion.identity;
        }
    }
}