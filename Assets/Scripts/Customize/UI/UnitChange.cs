using Customize;
using UnityEngine;

public class UnitChange : MonoBehaviour
{
    private Manager Manager { get => Manager.Instance; }
    void Start() { }

    public void Change(int num)
    {
        Destroy(Manager.unit);
        Manager.createManager.UnitCreate(Manager.group.allyTeam[num]);
    }
}
