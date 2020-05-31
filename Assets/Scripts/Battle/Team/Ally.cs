using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ally : MonoBehaviour
{
    private List<UnitStatus> battleQueue;
    private UnitStatus currentUnit;
    private UnitStatus targetUnit;

    public void Init(List<UnitStatus> _battleQueue)
    {
        battleQueue = _battleQueue;
    }

    public void Turn()
    {
        currentUnit = BattleManager.currentUnit;

        bool isTarget = SetTargetUnit();
        if (isTarget) 
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Skip());
        }
    }

    private bool SetTargetUnit() 
    {        
        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {            
            return false;
        }        

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return false;
        }

        bool isUnit = hit.transform.CompareTag("Ally") || hit.transform.CompareTag("Enemy");
        if (!isUnit)
        {
            return false;
        }

        targetUnit = battleQueue.First(x => x.gameObject.name == hit.transform.gameObject.name);
        bool isTarget = currentUnit.target.Contains(targetUnit.place);
        if (!isTarget)
        {
            return false;
        }

        BattleManager.targetUnit = targetUnit;
        return true;
    }

    public IEnumerator Attack()
    {
        StartCoroutine(currentUnit.gameObject.GetComponent<Personage>().UnitRotation());        
        yield return new WaitForSeconds(.5f);
        currentUnit.gameObject.GetComponent<Personage>().Attack(targetUnit);
        
        StartCoroutine(currentUnit.gameObject.GetComponent<Personage>().UnitPosition(1));
        yield return new WaitForSeconds(.5f);

        BattleManager.phase = "End";
    }

    public IEnumerator Skip()
    {
        currentUnit.gameObject.GetComponent<Personage>().Skip();
        yield return new WaitForSeconds(.25f);

        BattleManager.phase = "End";
    }
}

//public class Ally : MonoBehaviour
//{    
//    private List<UnitStatus> battleQueue;
//    private UnitStatus targetUnit;
//    private bool isAttack = false;

//    public void Init(List<UnitStatus> _battleQueue)
//    {
//        battleQueue = _battleQueue;
//    }

//    public void Turn(UnitStatus currentUnit) 
//    {
//        if (Input.GetKeyDown(KeyCode.Mouse0))
//        {           
//            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
//            bool isUnit = false;
//            bool condition = false;

//            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
//            {                
//                isUnit = hit.transform.CompareTag("Ally") || hit.transform.CompareTag("Enemy");

//                targetUnit = battleQueue.First(x => x.gameObject.name == hit.transform.gameObject.name);
//                condition = currentUnit.gameObject.GetComponent<Personage>().CheckTarget(currentUnit, targetUnit);                
//            }

//            if (!isUnit || !condition)
//            {
//                return;
//            }

//            isAttack = true;
//        }        

//        if (Input.GetKeyDown(KeyCode.D))
//        {
//            StartCoroutine(Skip(currentUnit));
//        }

//        if (isAttack)
//        {
//            StartCoroutine(Attack(currentUnit, targetUnit));
//            AttackAnimate(currentUnit);
//        }        
//    }

//    public IEnumerator Attack(UnitStatus currentUnit, UnitStatus targetUnit)
//    {        
//        currentUnit.gameObject.transform.rotation = Quaternion.Slerp(currentUnit.gameObject.transform.rotation, Quaternion.LookRotation(targetUnit.gameObject.transform.position - currentUnit.gameObject.transform.position), 5 * Time.deltaTime);
//        yield return new WaitForSeconds(.5f);
//        isAttack = false;
//    }

//    private void AttackAnimate(UnitStatus currentUnit)
//    {
//        Debug.Log("Test");

//        if (isAttack)
//        {
//            return;
//        }

//        Debug.Log("Attack");
//        currentUnit.gameObject.GetComponent<Personage>().Attack(targetUnit);
//        BattleManager.phase = "End";
//    }

//    public IEnumerator Skip(UnitStatus currentUnit)
//    {
//        currentUnit.gameObject.GetComponent<Personage>().Skip();
//        yield return new WaitForSeconds(.25f);

//        BattleManager.phase = "End";
//    }
//}
