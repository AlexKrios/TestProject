using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    void Start() { }

    public void HpIndicator(UnitStatus unitStatus)
    {
        var canvas = unitStatus.model.gameObject.transform.Find("Canvas").gameObject;
        var hpBar = canvas.transform.Find("HpBg/HpBar").GetComponent<Image>();

        hpBar.fillAmount = (float)(100 / unitStatus.hp * unitStatus.currentHp) / 100;
        canvas.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    public void HpInit(List<UnitStatus> battleQueue)
    {
        foreach (UnitStatus unitStatus in battleQueue)
        {
            HpIndicator(unitStatus);
        }
    }
}
