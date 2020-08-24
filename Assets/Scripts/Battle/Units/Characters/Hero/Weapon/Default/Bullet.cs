using Battle.Units.Characters;
using Units.Animations;
using Units.Objects.BattleUnit;
using UnityEngine;

namespace Battle.Units.Hero.Weapon.Default
{
    public class Bullet : BulletAbstract
    {
        private Manager Manager { get => Manager.Instance; }
        private BattleUnitObject CurrentUnit { get => Manager.currentUnit; }
        private BattleUnitObject TargetUnit { get => Manager.targetUnit; }

        void Update()
        {
            float step = 50 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetUnit.UnitGO.transform.position, step);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject == TargetUnit.UnitGO)
            {
                CurrentUnit.UnitGO.GetComponent<IUnitDamage>().Damage();
                TargetUnit.UnitGO.GetComponent<IAnimHit>().HitOrDead();
                Destroy(gameObject);
            }
        }
    }
}

