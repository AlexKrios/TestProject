using System.Collections.Generic;
using System.Linq;
using Units.Animations;
using Units.Objects.BattleUnit;
using UnityEngine;

namespace Battle.Units.Turn
{
    public class EnemyTurn : MonoBehaviour, IEnemyTurn
    {
        private Manager Manager { get => Manager.Instance; }
        public List<BattleUnitObject> UnitsList { get => Manager.unitsList; }
        private BattleUnitObject CurrentUnit { get => Manager.currentUnit; }

        private void Start() { }

        public void Execute()
        {
            if (CurrentUnit.Team != "Enemy")
            {
                return;
            }

            if (CurrentUnit.Target.Count == 0)
            {
                CurrentUnit.UnitGO.GetComponent<IAnimTurnEnd>().TurnEnd();
                return;
            }            

            Manager.AddBattleStatus("UnitAttack");

            var rIndex = Random.Range(0, CurrentUnit.Target.Count);
            var place = CurrentUnit.Target[rIndex];

            Manager.targetUnit = UnitsList.First(x => x.Id == place);

            CurrentUnit.UnitGO.GetComponent<IUnitAttack>().Attack();
        }
    }
}
