using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject cam;

    public static float globalSpeed = 1;
    public static BattleStateEnum battlePhase = BattleStateEnum.CameraStart;

    /* Army arrays */
    private UnitStatus[] _allyArmy;
    private UnitStatus[] _enemyArmy;

    public List<UnitStatus> battleQueue = null;
    public static UnitStatus currentUnit = null;
    public static UnitStatus targetUnit = null;    

    /* Submodule classes */
    public BattleStart bs;
    public BattleQueue bq;
    public BattleMark bm;    
    public BattleUI bui;
    public BattleAnimation ba;

    public UnitBehaviour ub;

    void Start()
    {        
        _allyArmy = bs.CreateArmy("Ally");
        _enemyArmy = bs.CreateArmy("Enemy");

        battleQueue = bq.CreateQueue(_allyArmy, _enemyArmy);
    }

    void Update()
    {
        bui.HpInit(battleQueue);

        switch (battlePhase)
        {
            case BattleStateEnum.CameraStart:
                return;
            case BattleStateEnum.TurnStart:
                TurnStart();                
                return;
            case BattleStateEnum.AnimationStart:
                ba.TurnStart();
                return;
            case BattleStateEnum.Turn:
                targetUnit = ub.Turn(currentUnit);
                return;
            case BattleStateEnum.AnimationTurn:
                ba.TurnAttackStart();
                return;
            case BattleStateEnum.AnimationDead:
                ba.Dead();
                return;
            case BattleStateEnum.TurnEnd:
                TurnEnd();
                return;
            case BattleStateEnum.AnimationEnd:
                ba.TurnEnd();
                return;
            case BattleStateEnum.BattleEnd:
                bq.BattleEnd();
                return;
        }        
    }

    private void TurnStart()
    {
        currentUnit = bq.CurrentUnit();
        bm.MarkedTarget(currentUnit);        

        battlePhase = BattleStateEnum.AnimationStart;
    }    

    private void TurnEnd() 
    {
        bm.DestroyTurnMark();

        battlePhase = BattleStateEnum.AnimationEnd;        

        bq.BattleEnd();        
    }

    public void Skip()
    {
        battlePhase = BattleStateEnum.TurnEnd;
    }
}
