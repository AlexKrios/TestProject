using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Battle.Units.Characters;
using Units.Animations;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour, IUnitTarget, IUnitMarked, IUnitAttack, IUnitDamage, IUnitSkip
    {
        protected Manager Manager { get => Manager.Instance; }
        protected UnitsMark UnitsMark { get => Manager.unitsMark; }
        protected List<UnitStatus> UnitsList { get => Manager.unitsList; }
        protected UnitStatus CurrentUnit { get => Manager.currentUnit; }
        protected UnitStatus TargetUnit { get => Manager.targetUnit; }

        public UnitData data;
        [NonSerialized] public int hp;
        [NonSerialized] public int attack;
        [NonSerialized] public string attackType;
        [NonSerialized] public int defence;
        [NonSerialized] public int initiative;
        [NonSerialized] public string type;

        void Awake()
        {
            hp = data.hp;
            attack = data.attack;
            attackType = data.attackType;
            defence = data.defence;
            initiative = data.initiative;
            type = data.type;
        }

        public virtual List<int> UnitTarget(string team)
        {
            var target = UnitsList
                .Where(x => x.status == "Live" && x.team != team)
                .Select(x => x.place)
                .ToList();

            return target;
        }

        public virtual void MarkedTargetAndSelf()
        {
            var currentPath = "Battle/Marks/CurrentMark";
            var targetPath = "Battle/Marks/EnemyMark";

            UnitsMark.Create(CurrentUnit, currentPath);
            foreach (int index in CurrentUnit.target)
            {
                var targetUnit = UnitsList.First(x => x.place == index);
                UnitsMark.Create(targetUnit, targetPath);
            }
        }

        public virtual void Attack()
        {
            gameObject.GetComponent<UnitAnimation>().OnAttackTarget.Invoke();
        }

        public virtual void Aim()
        {
            var weaponModel = CurrentUnit.weapon.transform.Find("Model").gameObject;
            StartCoroutine(weaponModel.GetComponent<IAim>().Aim());
        }

        public virtual void Damage()
        {
            TargetUnit.currentHp -= attack / CurrentUnit.aimCount;
            Manager.uiManager.HpChange(TargetUnit);

            if (TargetUnit.currentHp <= 0)
            {
                TargetUnit.status = "Dead";
            }
        }

        public virtual void Skip() { }

        public void ShowStat()
        {
            Debug.Log($"Hp: {hp}, Attack: {attack}, Defence: {defence}, Initiative: {initiative}");
            //Debug.Log($"Type: {type}");
        }        
    }
}