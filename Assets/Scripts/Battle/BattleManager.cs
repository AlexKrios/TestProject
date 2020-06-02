using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    CameraStart = 0,
    TurnStart = 1,
    AnimationStart = 2,
    Turn = 3,
    AnimationTurn = 4,
    TurnEnd = 5,
    AnimationEnd = 6,
    BattleEnd = 7
}

public class BattleManager : MonoBehaviour
{
    public GameObject cam;

    public static float globalSpeed = 1;
    public static BattleState battlePhase = BattleState.TurnStart;

    /* Army arrays */
    private UnitStatus[] allyArmy;
    private UnitStatus[] enemyArmy;

    public List<UnitStatus> battleQueue = null;
    public static UnitStatus currentUnit = null;
    public static UnitStatus targetUnit = null;    

    /* Submodule classes */
    public BattleCamera bc;
    public BattleStart bs;
    public BattleQueue bq;
    public BattleMark bm;    
    public BattleUI bui;
    public BattleAnimation ba;

    public UnitBehaviour ub;

    void Start()
    {        
        allyArmy = bs.CreateArmy("Ally");
        enemyArmy = bs.CreateArmy("Enemy");

        battleQueue = bq.CreateQueue(allyArmy, enemyArmy);

        bc.StartCamera();        
    }

    void Update()
    {
        switch (battlePhase)
        {
            case BattleState.CameraStart:
                ba.CameraStart();
                return;
            case BattleState.TurnStart:
                TurnStart();                
                return;
            case BattleState.AnimationStart:
                ba.TurnStart();
                return;
            case BattleState.Turn:
                targetUnit = ub.Turn(currentUnit);
                return;
            case BattleState.AnimationTurn:
                ba.Turn();
                return;
            case BattleState.TurnEnd:
                TurnEnd();
                return;
            case BattleState.AnimationEnd:
                ba.TurnEnd();
                return;
            case BattleState.BattleEnd:
                bq.BattleEnd();
                return;
        }
    }

    private void TurnStart()
    {
        currentUnit = bq.CurrentUnit();
        bm.MarkedTarget(currentUnit);        
        bui.HpInit(battleQueue);        

        battlePhase = BattleState.AnimationStart;
    }    

    private void TurnEnd() {                
        battlePhase = BattleState.AnimationEnd;

        bm.DestroyTurnMark();
        bq.BattleEnd();        
    }

    public void Skip()
    {
        battlePhase = BattleState.TurnEnd;
    }
}
