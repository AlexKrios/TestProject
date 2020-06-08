using UnityEngine;

public enum BattleStateEnum
{
    CameraStart = 0,
    TurnStart = 1,
    AnimationStart = 2,
    Turn = 3,
    AnimationTurn = 4,
    AnimationDead = 5,
    TurnEnd = 6,
    AnimationEnd = 7,
    BattleEnd = 8
}

public class BattleState : MonoBehaviour
{
    public void BattlePhaseCameraStart()
    {
        BattleManager.battlePhase = BattleStateEnum.CameraStart;
    }

    public void BattlePhaseTurnStart()
    {
        BattleManager.battlePhase = BattleStateEnum.TurnStart;
    }

    public void BattlePhaseAnimationStart()
    {
        BattleManager.battlePhase = BattleStateEnum.AnimationStart;
    }

    public void BattlePhaseTurn()
    {
        BattleManager.battlePhase = BattleStateEnum.Turn;
    }

    public void BattlePhaseAnimationTurn()
    {
        BattleManager.battlePhase = BattleStateEnum.AnimationTurn;
    }

    public void BattlePhaseAnimationDead()
    {
        if (BattleManager.targetUnit.status == "Dead")
        {
            BattleManager.battlePhase = BattleStateEnum.AnimationDead;
            return;
        }

        BattleManager.battlePhase = BattleStateEnum.TurnEnd;
    }

    public void BattlePhaseTurnEnd()
    {
        BattleManager.battlePhase = BattleStateEnum.TurnEnd;
    }

    public void BattlePhaseAnimationEnd()
    {
        BattleManager.battlePhase = BattleStateEnum.AnimationEnd;
    }

    public void BattlePhaseBattleEnd()
    {
        BattleManager.battlePhase = BattleStateEnum.BattleEnd;
    }
}
