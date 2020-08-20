using System.Collections.Generic;
using System.Linq;
using Units.Animations;
using UnityEngine;

namespace Battle.Units.Turn
{
    public class EnemyTurn : MonoBehaviour, IEnemyTurn
    {
        private Manager Manager { get => Manager.Instance; }
        public List<UnitStatus> UnitsList { get => Manager.unitsList; }
        private UnitStatus CurrentUnit { get => Manager.currentUnit; }

        private void Start() { }

        public void Execute()
        {
            //if (CurrentUnit.team != "Enemy")
            //{
            //    return;
            //}

            if (CurrentUnit.target.Count == 0)
            {
                CurrentUnit.gameObject.GetComponent<IAnimTurnEnd>().TurnEnd();
                return;
            }            

            Manager.AddBattleStatus("UnitAttack");

            var rIndex = Random.Range(0, CurrentUnit.target.Count);
            var place = CurrentUnit.target[rIndex];

            Manager.targetUnit = UnitsList.First(x => x.place == place);

            CurrentUnit.gameObject.GetComponent<IUnitAttack>().Attack();
        }
    }
}
