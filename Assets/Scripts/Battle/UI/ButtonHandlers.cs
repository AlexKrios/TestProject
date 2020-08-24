using Battle;
using Units.Animations;
using Units.Objects.BattleUnit;
using UnityEngine;

public class ButtonHandlers : MonoBehaviour
{
    private Manager Manager { get => Manager.Instance; }
    private BattleUnitObject CurrentUnit { get => Manager.currentUnit; }

    public void Execute()
    {
        CurrentUnit.UnitGO.GetComponent<IAnimTurnEnd>().TurnEnd();
    }
}
