using Battle.Create;
using Battle.Load;
using Battle.Save;
using Battle.Units;
using Battle.Units.Turn;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void Start()
        {
            Instance = this;

            var allArmy = armyCreate.CreateArmy(_group.allyTeam);
            var enemyArmy = armyCreate.CreateArmy(_group.enemyTeam);

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
