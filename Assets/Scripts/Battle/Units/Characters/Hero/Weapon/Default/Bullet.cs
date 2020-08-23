﻿using Battle.Units.Characters;
using Units.Animations;
using UnityEngine;

namespace Battle.Units.Hero.Weapon.Default
{
    public class Bullet : BulletAbstract
    {
        private Manager Manager { get => Manager.Instance; }
        private UnitStatus CurrentUnit { get => Manager.currentUnit; }
        private UnitStatus TargetUnit { get => Manager.targetUnit; }

        void Update()
        {
            float step = 50 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetUnit.gameObject.transform.position, step);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject == TargetUnit.gameObject)
            {
                CurrentUnit.gameObject.GetComponent<IUnitDamage>().Damage();
                TargetUnit.gameObject.GetComponent<IAnimHit>().HitOrDead();
                Destroy(gameObject);
            }
        }
    }
}
