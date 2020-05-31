using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<UnitStatus> battleQueue;
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

        isTurn = true;

        if (currentUnit.target.Count == 0)
        {
            StartCoroutine(Skip(currentUnit));
            return;
        }        

        StartCoroutine(Attack(currentUnit));
    }

    public IEnumerator Attack(UnitStatus currentUnit)
    {
        var randomIndex = Random.Range(0, currentUnit.target.Count);
        var place = currentUnit.target[randomIndex];
        BattleManager.targetUnit = battleQueue.First(x => x.place == place);

        StartCoroutine(currentUnit.gameObject.GetComponent<Personage>().UnitRotation());
        yield return new WaitForSeconds(.5f);
        currentUnit.gameObject.GetComponent<Personage>().Attack(BattleManager.targetUnit);

        StartCoroutine(currentUnit.gameObject.GetComponent<Personage>().UnitPosition(1));
        yield return new WaitForSeconds(.5f);        

        isTurn = false;
        BattleManager.phase = "End";
    }

    public IEnumerator Skip(UnitStatus currentUnit)
    {
        currentUnit.gameObject.GetComponent<Personage>().Skip();
        yield return new WaitForSeconds(1);

        isTurn = false;   
        BattleManager.phase = "End";
    }
}
