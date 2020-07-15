using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Units.Behaviour
{
    public class ClickTarget : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        private List<UnitStatus> UnitsList { get => Manager.unitsList; }
        private UnitStatus CurrentUnit { get => Manager.currentUnit; }

        public void Click()
        {
            if (Manager.battleStatus.Count != 0 || !IsNeedPlace())
            {
                return;
            }

            Manager.targetUnit = SetTargetUnit();
            Manager.AddBattleStatus("UnitAttack");

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
    }
}
