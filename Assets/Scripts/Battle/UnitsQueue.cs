using Battle.Units;
using System.Collections.Generic;
using System.Linq;
using Units.Objects.BattleUnit;
using UnityEngine;

namespace Battle
{
    public class UnitsQueue : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        public List<BattleUnitObject> UnitsList { get => Manager.unitsList; }

        private void Start() { }

        public List<BattleUnitObject> CreateQueue(List<BattleUnitObject> allyArmy, List<BattleUnitObject> enemyArmy)
        {
            var units = new List<BattleUnitObject>();
            units.AddRange(allyArmy);                                                       //Added ally to list
            units.AddRange(enemyArmy);                                                      //Added enemy to list

            units.RemoveAll(x => x == null);                                                //Remove empty cell

            var sortedUnits = units.OrderByDescending(u => u.Unit.Initiative).ToList();     //Sort list by initiative

            return sortedUnits;
        }

        public BattleUnitObject SetCurrentUnit()
        {
            var currentUnit = UnitsList.FirstOrDefault(x => x.Turn == true && x.Status == "Live");         
            if (currentUnit == null)
            {
                RefreshQueue();
                currentUnit = UnitsList.First(x => x.Turn == true && x.Status == "Live");
            }
            
            currentUnit.Turn = false;
            var currentUnitTarget = currentUnit.UnitGO.GetComponent<IUnitTarget>();
            currentUnit.Target = currentUnitTarget.UnitTarget(currentUnit.Team);

            return currentUnit;
        }

        private void RefreshQueue()
        {
            foreach (BattleUnitObject unit in UnitsList)
            {
                unit.Turn = true;
            }
        }

        private void QueueOutput()
        {
            foreach (BattleUnitObject unit in UnitsList)
            {
                if (unit == null)
                {
                    Debug.Log($"Null");
                    return;
                }

                Debug.Log($"{unit.UnitGO} - {unit.Status} - {unit.Turn}");
            }
        }
    }
}
