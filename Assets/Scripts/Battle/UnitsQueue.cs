using Battle.Units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class UnitsQueue : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        public List<UnitStatus> UnitsList { get => Manager.unitsList; }

        private void Start() { }

        public List<UnitStatus> CreateQueue(List<UnitStatus> allyArmy, List<UnitStatus> enemyArmy)
        {
            var units = new List<UnitStatus>();
            units.AddRange(allyArmy);                                                   //Added ally to list
            units.AddRange(enemyArmy);                                                  //Added enemy to list

            units.RemoveAll(x => x == null);                                            //Remove empty cell

            var sortedUnits = units.OrderByDescending(u => u.initiative).ToList();      //Sort list by initiative

            return sortedUnits;
        }

        public UnitStatus SetCurrentUnit()
        {
            var currentUnit = UnitsList.FirstOrDefault(x => x.turn == true && x.status == "Live");         
            if (currentUnit == null)
            {
                RefreshQueue();
                currentUnit = UnitsList.First(x => x.turn == true && x.status == "Live");
            }
            
            currentUnit.turn = false;
            var currentUnitTarget = currentUnit.gameObject.GetComponent<IUnitTarget>();
            currentUnit.target = currentUnitTarget.UnitTarget(currentUnit.team);

            return currentUnit;
        }

        private void RefreshQueue()
        {
            foreach (UnitStatus unit in UnitsList)
            {
                unit.turn = true;
            }
        }

        private void QueueOutput()
        {
            foreach (UnitStatus unit in UnitsList)
            {
                if (unit == null)
                {
                    Debug.Log($"Null");
                    return;
                }

                Debug.Log($"{unit.gameObject} - {unit.status} - {unit.turn}");
            }
        }
    }
}
