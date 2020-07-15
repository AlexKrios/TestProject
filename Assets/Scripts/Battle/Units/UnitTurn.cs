using Battle.Units.Behaviour;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Units
{
    public class UnitTurn : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        public List<UnitStatus> UnitsList { get => Manager.unitsList; }
        private UnitStatus CurrentUnit { get => Manager.currentUnit; }

        public void Execute()
        {
            if (CurrentUnit.team != "Enemy")
            {
                return;
            }

            Manager.AddBattleStatus("UnitAttack");

            var rIndex = Random.Range(0, CurrentUnit.target.Count);
            var place = CurrentUnit.target[rIndex];

            Manager.targetUnit = UnitsList.First(x => x.place == place);

            gameObject.GetComponent<IUnitAttack>().Attack();
        }

        public void Click()
        {
            if (Manager.battleStatus.Count != 0 || !IsNeedPlace())
            {
                return;
            }

            Manager.AddBattleStatus("UnitAttack");
            Manager.targetUnit = SetTargetUnit();            

            gameObject.GetComponent<IUnitAttack>().Attack();
        }

        private bool IsNeedPlace()
        {
            var tempTarget = SetTargetUnit();
            return CurrentUnit.target.Contains(tempTarget.place);
        }

        private UnitStatus SetTargetUnit()
        {
            return UnitsList.First(x => x.gameObject == gameObject);
        }

        #region Call turn event
        public void TurnInit()
        {
            Manager.OnTurnInit.Invoke();
        }

        public void TurnStart()
        {
            Manager.OnTurnStart.Invoke();
        }
        #endregion
    }
}
