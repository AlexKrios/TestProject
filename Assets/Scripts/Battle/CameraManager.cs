using Battle;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Manager Manager { get => Manager.Instance; }

    public void TurnStart()
    {
        Manager.RemoveBattleStatus("CameraMove");
        Manager.OnTurnInit.Invoke();
    }
}
