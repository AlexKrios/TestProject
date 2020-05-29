using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject cam;

    private UnitStatus[] allyArmy;
    private UnitStatus[] enemyArmy;

    public List<UnitStatus> battleQueue;
    public UnitStatus currentUnit;

    public static string phase = "Start";

    /* Submodule classes */
    private BattleStart bs;
    private BattleQueue bq;
    private BattleMark bm;
    private BattleCamera bc;
    private BattleUI bui;

    private Ally ally;
    private Enemy enemy;

    void Start()
    {        
        bs = cam.AddComponent<BattleStart>();
        bq = cam.AddComponent<BattleQueue>();
        bm = cam.AddComponent<BattleMark>();
        bc = cam.AddComponent<BattleCamera>();
        bui = cam.AddComponent<BattleUI>();

        allyArmy = bs.CreateArmy("Ally");
        enemyArmy = bs.CreateArmy("Enemy");

        battleQueue = bq.CreateQueue(allyArmy, enemyArmy);

        ally = cam.AddComponent<Ally>();
        enemy = cam.AddComponent<Enemy>();
        ally.Init(battleQueue);
        enemy.Init(battleQueue);

        bc.StartCamera();
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
        bui.HpInit();

        phase = "Turn";
    }

    private void Turn()
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
        }
        else if (isEnemy)
        {
            enemy.Turn(currentUnit);
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

    public void Skip()
    {
        ally.Skip(currentUnit);
    }
}
