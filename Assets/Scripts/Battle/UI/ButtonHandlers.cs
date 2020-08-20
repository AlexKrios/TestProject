using Battle;
using Battle.Units;
using Battle.Units.Turn;
using Units.Animations;
using UnityEngine;

public class ButtonHandlers : MonoBehaviour
{
    private Manager Manager { get => Manager.Instance; }
    private UnitStatus CurrentUnit { get => Manager.currentUnit; }

    public void Execute()
    {
        CurrentUnit.gameObject.GetComponent<IAnimTurnEnd>().TurnEnd();
    }
}
