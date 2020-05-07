using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject cam;

    private UnitStatus[] allyArmy;
    private UnitStatus[] enemyArmy;

    public List<UnitStatus> battleQueue = new List<UnitStatus>();
    public UnitStatus currentUnit;

    public static string phase = "Start";

    /* Submodule classes */    
    private BattleStart bs;
    private BattleQueue bq;
    private BattleMark bm;

    private UnitUI ui;

    private Ally ally;
    private Enemy enemy;

    void Start()
    {        
        bs = cam.AddComponent<BattleStart>();
        bq = cam.AddComponent<BattleQueue>();
        bm = cam.AddComponent<BattleMark>();

        allyArmy = bs.CreateArmy("Ally");
        enemyArmy = bs.CreateArmy("Enemy");

        battleQueue = bq.CreateQueue(allyArmy, enemyArmy);

        ally = new Ally(battleQueue);
        enemy = new Enemy();

        ui = new UnitUI(cam);
    }

    void Update()
    {        
        TurnStart();
        Turn();
        TurnEnd();
    }

    private void TurnStart()
    {
        if (phase != "Start")
        {
            return;
        }

        currentUnit = bq.CurrentUnit();
        bm.MarkedTarget(currentUnit);
        ui.HpInit();

        phase = "Turn";
    }

    public void Turn()
    {
        if (phase != "Turn")
        {
            return;
        }

        bool isAlly = currentUnit.team == "Ally";
        bool isEnemy = currentUnit.team == "Enemy";

        if (isAlly)
        {
            ally.Turn(currentUnit);
            return;
        }
        else if (isEnemy)
        {
            enemy.Turn();
            return;
        }
    }

    private void TurnEnd() {
        if (phase != "End")
        {
            return;
        }        

        bm.DestroyTurnMark();

        phase = "Start";

        bq.BattleEnd();
    }
}
