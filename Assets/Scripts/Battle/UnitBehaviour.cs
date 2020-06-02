using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    private List<UnitStatus> battleQueue;
    private UnitStatus _currentUnit;
    private UnitStatus _targetUnit;

    void Start()
    {
        battleQueue = GameObject.Find("MainCamera").GetComponent<BattleManager>().battleQueue;
    }

    public UnitStatus Turn(UnitStatus currentUnit)
    {
        _currentUnit = currentUnit;

        bool isAlly = currentUnit.team == "Ally";
        bool isEnemy = currentUnit.team == "Enemy";

        if (isAlly)
        {
            _targetUnit = Ally();
        }
        else if (isEnemy)
        {
            _targetUnit = Enemy();
        }

        return _targetUnit; 
    }

    private UnitStatus Ally()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        bool inputMouse = Input.GetKeyDown(KeyCode.Mouse0);
        bool raycastHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
        if (!inputMouse || !raycastHit)
        {
            return null;
        }

        bool isUnit = hit.transform.CompareTag("Ally") || hit.transform.CompareTag("Enemy");
        if (!isUnit)
        {
            return null;
        }

        _targetUnit = battleQueue.First(x => x.model.gameObject.name == hit.transform.gameObject.name);
        bool isTarget = _currentUnit.target.Contains(_targetUnit.place);
        if (!isTarget)
        {
            return null;
        }

        BattleManager.battlePhase = BattleState.AnimationTurn;
        return _targetUnit;
    }

    private UnitStatus Enemy()
    {
        var randomIndex = Random.Range(0, _currentUnit.target.Count);
        var place = _currentUnit.target[randomIndex];

        BattleManager.battlePhase = BattleState.AnimationTurn;
        return battleQueue.First(x => x.place == place);
    }
}