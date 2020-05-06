using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private Camera cam;

    private Ray ray;
    private RaycastHit hit;

    private List<UnitStatus> battleQueue;

    /* Submodule classes */
    protected BattleMark battleMark = new BattleMark();

    void Start() {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        battleQueue = cam.GetComponent<BattleQueue>().battleQueue;
    }

    public void AllyTurn(UnitStatus currentUnit)
    {
        bool isEnemyTurn = BattleManager.phase != "Turn" || currentUnit.team == "Enemy";
        if (isEnemyTurn) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

            bool condition = CheckTarget(currentUnit);
            if (!condition)
            {
                return;
            }            

            Action(currentUnit);

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
            bool isUnit = hit.transform.tag != "Ally" && hit.transform.tag != "Enemy";

            if (isUnit)
            {
                return false;
            }

            string hitName = hit.transform.gameObject.name;
            UnitStatus targetUnit = battleQueue.First(x => x.gameObject.name == hitName);

            condition = currentUnit.gameObject.GetComponent<Personage>().CheckTarget(currentUnit, targetUnit);
        }        

        return condition;
    }

    private void Action(UnitStatus currentUnit) 
    {
        string hitName = hit.transform.gameObject.name;
        UnitStatus targetUnit = battleQueue.First(x => x.gameObject.name == hitName);

        currentUnit.gameObject.GetComponent<Personage>().Attack(targetUnit);

        if (targetUnit.currentHp <= 0) 
        {
            targetUnit.status = "Dead";
            Destroy(targetUnit.gameObject);
        }
    }

    public void EnemyTurn(UnitStatus currentUnit) 
    {
        bool isAllyTurn = BattleManager.phase != "Turn" || currentUnit.team == "Ally";

        if (isAllyTurn)
        {
            return;
        }

        BattleManager.phase = "End";
    }

    public void MarkedTarget(UnitStatus currentUnit) {
        if (currentUnit.team == "Enemy") 
        {
            return;
        }

        battleMark.Create(currentUnit);
        foreach (int index in currentUnit.target)
        {
            currentUnit.gameObject.GetComponent<Personage>().MarkedTarget(index);
        }  
    }

    public void DestroyTurnMark()
    {
        GameObject[] marks = GameObject.FindGameObjectsWithTag("TurnMark");
        foreach (GameObject mark in marks)
        {
            Destroy(mark);
        }
    }
}
