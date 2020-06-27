using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public Image globalHpBarAlly;
    public Text globalHpTextAlly;

    public Image globalHpBarEnemy;
    public Text globalHpTextEnemy;

    void Start() { }

    public void GlobalHp()
    {
        var currentUnit = BattleManager.currentUnit;
        var targetUnit = BattleManager.targetUnit;

        /* Ally HP */
        if (currentUnit != null)
        {
            globalHpBarAlly.fillAmount = (float)(100 / currentUnit.hp * currentUnit.currentHp) / 100;
            globalHpTextAlly.text = $"{currentUnit.currentHp}/{currentUnit.hp}";
        }

        /* Enemy HP */
        if (targetUnit != null)
        {
            globalHpBarEnemy.fillAmount = (float)(100 / targetUnit.hp * targetUnit.currentHp) / 100;
            globalHpTextEnemy.text = $"{targetUnit.currentHp}/{targetUnit.hp}";
        }
        else
        {
            globalHpBarEnemy.fillAmount = 0;
            globalHpTextEnemy.text = "";
        }
    }

    public void HpIndicator(UnitStatus unitStatus)
    {
        var canvas = unitStatus.gameObject.transform.Find("Canvas").gameObject;
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
