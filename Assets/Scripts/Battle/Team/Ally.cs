using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ally : MonoBehaviour
{    
    private List<UnitStatus> battleQueue;
    private UnitStatus targetUnit;
    private bool isTurn = false;

    public void Init(List<UnitStatus> _battleQueue)
    {
        battleQueue = _battleQueue;
    }

    public void Turn(UnitStatus currentUnit) 
    {
        if (isTurn)
        {
            return;
        }        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {           
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            bool isUnit = false;
            bool condition = false;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {                
                isUnit = hit.transform.CompareTag("Ally") || hit.transform.CompareTag("Enemy");

                targetUnit = battleQueue.First(x => x.gameObject.name == hit.transform.gameObject.name);
                condition = currentUnit.gameObject.GetComponent<Personage>().CheckTarget(currentUnit, targetUnit);                
            }

            if (!isUnit || !condition)
            {
                return;
            }            

            StartCoroutine(Attack(currentUnit, targetUnit));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Skip(currentUnit));
        }
    }

    public IEnumerator Attack(UnitStatus currentUnit, UnitStatus targetUnit)
    {
        isTurn = true;

        currentUnit.gameObject.GetComponent<Personage>().Attack(targetUnit);
        yield return new WaitForSeconds(.25f);

        if (targetUnit.currentHp <= 0)
        {
            targetUnit.status = "Dead";
            Destroy(targetUnit.gameObject);
        }

        isTurn = false;
        BattleManager.phase = "End";
    }

    public IEnumerator Skip(UnitStatus currentUnit)
    {
        isTurn = true;

        currentUnit.gameObject.GetComponent<Personage>().Skip();
        yield return new WaitForSeconds(.25f);

        isTurn = false;
        BattleManager.phase = "End";
    }
}
