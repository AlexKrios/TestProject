using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ally
{
    Ray ray;
    RaycastHit hit;

    private readonly List<UnitStatus> battleQueue;
    private UnitStatus currentUnit;
    private UnitStatus targetUnit;

    public Ally(List<UnitStatus> battleQueue)
    {
        this.battleQueue = battleQueue;
    }

    public void Turn(UnitStatus currentUnit) 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

            bool condition = CheckTarget(currentUnit);
            if (!condition)
            {
                return;
            }

            currentUnit.gameObject.GetComponent<Personage>().Attack(targetUnit);

            BattleManager.phase = "End";
            return;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            BattleManager.phase = "End";
        }
    }

    private bool CheckTarget(UnitStatus currentUnit)
    {
        bool condition = false;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            bool isUnit = hit.transform.tag == "Ally" || hit.transform.tag == "Enemy";

            if (!isUnit)
            {
                return false;
            }

            targetUnit = battleQueue.First(x => x.gameObject.name == hit.transform.gameObject.name);
            condition = currentUnit.gameObject.GetComponent<Personage>().CheckTarget(currentUnit, targetUnit);
        }

        return condition;
    }
}
