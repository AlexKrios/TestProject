using Customize;
using UnityEngine;

public class SelectorHandler : MonoBehaviour
{
    private Manager Manager { get => Manager.Instance; }

    void Start() { }

    public void ChangeUnit(int num)
    {
        Destroy(Manager.unit);
        Manager.createManager.CreateUnit(Manager.unitList[num]);
    }

    public void ChangeWeapon(int num)
    {
        Destroy(Manager.weapon.transform.Find("Model").gameObject);
        Manager.createManager.weapon.Execute(Manager.weaponList[num]);
    }
}
