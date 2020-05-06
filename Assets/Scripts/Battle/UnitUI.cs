using UnityEngine;
using UnityEngine.UI;

public class UnitUI
{
    public void HpIndicator(UnitStatus unitStatus) 
    {
        //GameObject canvasGo = new GameObject("Canvas");
        //canvasGo.transform.parent = unit.gameObject.transform;
        //Canvas canvas = canvasGo.AddComponent<Canvas>();
        //canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //GameObject hp = new GameObject("Hp");
        //hp.transform.parent = canvas.gameObject.transform;
        //hp.transform.position = unit.gameObject.transform.position;
        //Text hpText = hp.AddComponent<Text>();
        //hpText.font = Resources.Load<Font>("arial");
        //hpText.text = "Test";

        var unit = unitStatus.gameObject.GetComponent<Personage>();

        unit.hpBar.text = unitStatus.currentHp.ToString();

        if (unitStatus.team == "Ally") {
            unit.hpBar.transform.rotation = Quaternion.Euler(45, -90, 0);
        }

        if (unitStatus.team == "Enemy")
        {
            unit.hpBar.transform.rotation = Quaternion.Euler(45, 90, 0);
        }
    }
}
