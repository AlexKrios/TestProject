using Battle.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    void Start() { }

    public void HpChange(UnitStatus unitStatus)
    {
        var hpBar = unitStatus.gameObject.transform.Find("Canvas/HpBar").GetComponent<Image>();        

        hpBar.fillAmount = (float)(100 / unitStatus.hp * unitStatus.currentHp) / 100;
    }

    public void HpInit(List<UnitStatus> battleQueue)
    {
        foreach (UnitStatus unitStatus in battleQueue)
        {
            HpChange(unitStatus);
        }
    }
}
