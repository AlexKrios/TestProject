using System.Collections.Generic;
using Units.Objects.BattleUnit;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    void Start() { }

    public void HpChange(BattleUnitObject unitStatus)
    {
        var hpBar = unitStatus.UnitGO.transform.Find("Canvas/HpBar").GetComponent<Image>();        

        hpBar.fillAmount = (float)(100 / unitStatus.Unit.Hp * unitStatus.Unit.CurrentHp) / 100;
    }

    public void HpInit(List<BattleUnitObject> battleQueue)
    {
        foreach (BattleUnitObject unitStatus in battleQueue)
        {
            HpChange(unitStatus);
        }
    }
}
