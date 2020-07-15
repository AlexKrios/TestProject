using Battle.Units;
using Battle.Units.Animations;
using Battle.Units.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;

        public float speed = 1;                                             //Game speed

        public List<UnitStatus> unitsList = null;                           //List of all unit objects and him property
        public UnitStatus currentUnit = null;                               //Unit gameobject which turn
        public UnitStatus targetUnit = null;                                //Unit gameobject whom attacked

        public List<string> battleStatus = new List<string>();

        /* Sub-modules */
        public UnitsLoad unitsLoad;
        public UnitsSave unitsSave;
        public UnitsCreate unitsCreate;
        public UnitsQueue unitsQueue;        
        public UnitsMark unitsMark;

        public BattleUI battleUI;

        [NonSerialized] public UnityEvent OnTurnInit = new UnityEvent();
        [NonSerialized] public UnityEvent OnTurnStart = new UnityEvent();

        private void Start()
        {
            Instance = this;

            var allyArmy = unitsCreate.CreateArmy("Ally");
            var enemyArmy = unitsCreate.CreateArmy("Enemy");

            unitsList = unitsQueue.CreateQueue(allyArmy, enemyArmy);
            AddBattleStatus("CameraMove");

            OnTurnInit.AddListener(TurnInit);
            OnTurnStart.AddListener(TurnStart);
        }

        private void Update()
        {
            battleUI.HpInit(unitsList);
        }

        public void TurnInit()
        {
            currentUnit = unitsQueue.SetCurrentUnit();
            targetUnit = null;
            unitsMark.DestroyTurnMark();

            currentUnit.gameObject.GetComponent<IUnitMarked>().MarkedTargetAndSelf();
            currentUnit.gameObject.GetComponent<IAnimTurnStart>().TurnStart();
        }

        public void TurnStart()
        {
            currentUnit.gameObject.GetComponent<UnitTurn>().Execute();
            RemoveBattleStatus("UnitAttack");
        }

        public void AddBattleStatus(string value)
        {
            battleStatus.Add(value);
        }

        public void RemoveBattleStatus(string value)
        {
            battleStatus.Remove(value);
        }
    }
}
