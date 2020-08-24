using Battle.Create;
using Battle.Load;
using Battle.Save;
using Battle.Units.Turn;
using Parsers;
using System.Collections.Generic;
using Units.Objects.BattleUnit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;

        public float speed = 1;                                             //Game speed

        public List<BattleUnitObject> unitsList = null;                     //List of all unit objects and him property
        public BattleUnitObject currentUnit = null;                         //Unit gameobject which turn
        public BattleUnitObject targetUnit = null;                          //Unit gameobject whom attacked

        public List<string> battleStatus = new List<string>();

        private readonly Group _group = new Group();

        /* Sub-modules */
        [Header("Create")]  
        public ArmyCreate armyCreate;

        [Header("Load")]    
        public UnitsLoad unitsLoad;

        [Header("Save")]    
        public UnitsSave unitsSave;

        [Header("Queue")]   
        public UnitsQueue unitsQueue;

        [Header("Mark")]    
        public UnitsMark unitsMark;                

        [Header("Turn")]
        public TurnStart turnStart;
        public TurnEnd turnEnd;

        [Header("UI")]      
        public UIManager uiManager;

        public ParseBattleUnitData parseBattleUnitData;

        private void Start()
        {
            Instance = this;

            var allArmy = armyCreate.CreateArmy("Data/Units/AllyBattleUnitList");
            var enemyArmy = armyCreate.CreateArmy("Data/Units/EnemyBattleUnitList");

            unitsList = unitsQueue.CreateQueue(allArmy, enemyArmy);

            uiManager.HpInit(unitsList);
        }

        public void AddBattleStatus(string value)
        {
            battleStatus.Add(value);
        }
        public void RemoveBattleStatus(string value)
        {
            battleStatus.Remove(value);
        }

        public void Restart() 
        {
            SceneManager.LoadScene("Battle");
        }
    }
}
