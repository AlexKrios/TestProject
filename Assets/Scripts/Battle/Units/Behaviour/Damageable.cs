using System.Collections.Generic;
using System.Linq;
using Units.Objects.BattleUnit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Units.Turn
{
    public class Damagable : MonoBehaviour, IPointerClickHandler
    {
        private Manager Manager { get => Manager.Instance; }
        public List<BattleUnitObject> UnitsList { get => Manager.unitsList; }
        private BattleUnitObject CurrentUnit { get => Manager.currentUnit; }

        private void Start() { }

        public void OnPointerClick(PointerEventData data)
        {
            if (Manager.battleStatus.Count != 0 || !IsNeedPlace())
            {
                return;
            }

            Manager.AddBattleStatus("UnitAttack");
            Manager.targetUnit = SetTargetUnit();

            CurrentUnit.UnitGO.GetComponent<IUnitAttack>().Attack();
        }

        private bool IsNeedPlace()
        {
            var tempTarget = SetTargetUnit();
            return CurrentUnit.Target.Contains(tempTarget.Id);
        }

        private BattleUnitObject SetTargetUnit()
        {
            return UnitsList.First(x => x.UnitGO == gameObject);
        }
    }
}
