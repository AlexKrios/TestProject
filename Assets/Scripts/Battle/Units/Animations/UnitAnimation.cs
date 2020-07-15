using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Battle.Units.Animations
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
            
            animator.SetTrigger("isHit");
        }

        private IEnumerator RotateToTarget()
        {
            /* Object transform */
            var currentUnitParent = CurrentUnit.parent.transform;
            var targetUnitParent = TargetUnit.parent.transform;

            /* Target rotation */
            var targetR = Quaternion.LookRotation(targetUnitParent.position - currentUnitParent.position);

            var delta = 1f;
            while (currentUnitParent.rotation != targetR)
            {
                currentUnitParent.rotation = Quaternion.RotateTowards(currentUnitParent.rotation, targetR, delta);

                delta -= 0.005f;
                yield return null;
            }

            CurrentUnit.gameObject.GetComponent<IAnimAttack>().Attack();
        }

        public IEnumerator RotateToDefault()
        {
            var currentUnitParent = CurrentUnit.parent.transform;

            var targetR = Quaternion.Euler(0, 90, 0);
            if (CurrentUnit.team == "Enemy")
            {
                targetR = Quaternion.Euler(0, -90, 0);
            }

            var delta = 1f;
            while (currentUnitParent.rotation != targetR)
            {
                currentUnitParent.rotation = Quaternion.RotateTowards(currentUnitParent.rotation, targetR, delta);

                delta -= 0.005f;
                yield return null;
            }

            CurrentUnit.gameObject.GetComponent<IAnimTurnEnd>().TurnEnd();
        }
    }
}