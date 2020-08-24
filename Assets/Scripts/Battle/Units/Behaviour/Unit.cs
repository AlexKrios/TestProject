using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Battle.Units.Characters;
using Units.Animations;
using Units.Objects.BattleUnit;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour, IUnitTarget, IUnitMarked, IUnitAttack, IUnitDamage, IUnitSkip
    {
        protected Manager Manager { get => Manager.Instance; }
        protected UnitsMark UnitsMark { get => Manager.unitsMark; }
        protected List<BattleUnitObject> UnitsList { get => Manager.unitsList; }
        protected BattleUnitObject CurrentUnit { get => Manager.currentUnit; }
        protected BattleUnitObject TargetUnit { get => Manager.targetUnit; }

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
                .Where(x => x.Status == "Live" && x.Team != team)
                .Select(x => x.Id)
                .ToList();

            return target;
        }

        public virtual void MarkedTargetAndSelf()
        {
            var currentPath = "Battle/Marks/CurrentMark";
            var targetPath = "Battle/Marks/EnemyMark";

            UnitsMark.Create(CurrentUnit, currentPath);
            foreach (int index in CurrentUnit.Target)
            {
                var targetUnit = UnitsList.First(x => x.Id == index);
                UnitsMark.Create(targetUnit, targetPath);
            }
        }

        public virtual void Attack()
        {
            gameObject.GetComponent<UnitAnimation>().OnAttackTarget.Invoke();
        }

        public virtual void Aim()
        {
            var weaponModel = CurrentUnit.Weapon.transform.Find("Model").gameObject;
            StartCoroutine(weaponModel.GetComponent<IAim>().Aim());
        }

        public virtual void Damage()
        {
            TargetUnit.Unit.CurrentHp -= attack / CurrentUnit.AimCount;
            Manager.uiManager.HpChange(TargetUnit);

            if (TargetUnit.Unit.CurrentHp <= 0)
            {
                TargetUnit.Status = "Dead";
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