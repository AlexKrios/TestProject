using System;
using Units.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Battle.Units.Turn
{    
    public class TurnStart : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        private UnitsQueue UnitsQueue { get => Manager.unitsQueue; }

        private UnitStatus CurrentUnit { get => Manager.currentUnit; }

        [NonSerialized] public UnityEvent OnStartExecute = new UnityEvent();
        [NonSerialized] public UnityEvent OnEndExecute = new UnityEvent();

        private void Start()
        {
            OnStartExecute.AddListener(StartExecute);
            OnEndExecute.AddListener(EndExecute);
        }

        public void StartExecute()
        {
            Manager.AddBattleStatus("TurnStart");

            /* Reset current and target unit */
            Manager.currentUnit = UnitsQueue.SetCurrentUnit();                              //Set new current unit
            Manager.targetUnit = null;                                                      //Reset target unit

            CurrentUnit.gameObject.GetComponent<IUnitMarked>().MarkedTargetAndSelf();       //Marked all units
            CurrentUnit.gameObject.GetComponent<IAnimTurnStart>().TurnStart();              //Start turnStart animation
        }

        public void EndExecute()
        {
            Manager.RemoveBattleStatus("TurnStart");

            CurrentUnit.gameObject.GetComponent<IEnemyTurn>().Execute();                    //If enemy turn, enemy move
        }
    }
}