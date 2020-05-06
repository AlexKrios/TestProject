using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject cam;

    private UnitStatus[] allyArmy;
    private UnitStatus[] enemyArmy;

    public List<UnitStatus> battleQueue = new List<UnitStatus>();
    private UnitStatus currentUnit;

    public static string phase = "Start";

    /* Submodule classes */    
    private BattleStart bs;
    private BattleQueue bq;
    private Battle battle;

    void Start()
    {        
        bs = cam.AddComponent<BattleStart>();
        bq = cam.AddComponent<BattleQueue>();
        battle = cam.AddComponent<Battle>();

        allyArmy = bs.CreateArmy("Ally");
        enemyArmy = bs.CreateArmy("Enemy");

        battleQueue = bq.CreateQueue(allyArmy, enemyArmy);
    }

    void Update()
    {
        TurnStart();
        battle.AllyTurn(currentUnit);
        battle.EnemyTurn(currentUnit);
        TurnEnd();
    }

    private void TurnStart()
    {
        if (phase != "Start")
        {
            return;
        }

        currentUnit = bq.CurrentUnit();
        battle.MarkedTarget(currentUnit);

        phase = "Turn";
    }

    private void TurnEnd() {
        if (phase != "End")
        {
            return;
        }        

        battle.DestroyTurnMark();

        phase = "Start";

        bq.BattleEnd();
    }
}
