using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Units.Turn
{
    public class Damagable : MonoBehaviour, IPointerClickHandler
    {
        private Manager Manager { get => Manager.Instance; }
        public List<UnitStatus> UnitsList { get => Manager.unitsList; }
        private UnitStatus CurrentUnit { get => Manager.currentUnit; }

        private void Start() { }

        public void OnPointerClick(PointerEventData data)
        {
            if (Manager.battleStatus.Count != 0 || !IsNeedPlace())
            {
                return;
            }

            Manager.AddBattleStatus("UnitAttack");
            Manager.targetUnit = SetTargetUnit();

            CurrentUnit.gameObject.GetComponent<IUnitAttack>().Attack();
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
