using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleQueue : MonoBehaviour
{
    public List<UnitStatus> battleQueue = new List<UnitStatus>();

    #region Create queue section
    public List<UnitStatus> CreateQueue(UnitStatus[] allyArmy, UnitStatus[] enemyArmy) 
    {
        AddToQueue(allyArmy);
        AddToQueue(enemyArmy);

        //QueueOutput();

        return battleQueue;
    }

    private void AddToQueue(UnitStatus[] army) 
    {
        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] == null)
            {
                continue;
            }

            battleQueue.Add(army[i]);
        }
    }
    #endregion

    public UnitStatus CurrentUnit() 
    {
        QueueSorted();
        UnitStatus currentUnit = battleQueue[0];

        foreach (UnitStatus unit in battleQueue) 
        {
            if (unit.turn == false) 
            {
                continue;
            }

            unit.turn = false;
            unit.target = unit.gameObject.GetComponent<Personage>().UnitTarget();

            return unit;
        }

        RefreshQueue();

        return currentUnit;
    }

    private void QueueSorted()
    {
        List<UnitStatus> tempBattleQueue = new List<UnitStatus>(battleQueue);
        var battleQueueSorted = tempBattleQueue.OrderByDescending(u => u.initiative);

        battleQueue.Clear();

        foreach (UnitStatus unit in battleQueueSorted)
        {
            if (unit == null || unit.status == "Dead")
            {
                continue;
            }

            battleQueue.Add(unit);
        }
    }

    private void RefreshQueue() 
    {
        foreach (UnitStatus unit in battleQueue)
        {
            unit.turn = true;
        }

        battleQueue[0].turn = false;
        battleQueue[0].target = battleQueue[0].gameObject.GetComponent<Personage>().UnitTarget();
    }

    public void BattleEnd()
    {
        int allyCount = 0;
        int enemyCount = 0;

        foreach (UnitStatus unit in battleQueue)
        {
            if (unit.status != "Live") 
            {
                continue;
            }

            if (unit.team == "Ally")
            {
                allyCount++;
            }
            else if (unit.team == "Enemy") 
            {
                enemyCount++;
            }            
        }

        if (allyCount == 0 || enemyCount == 0) 
        {
            BattleManager.phase = "BattleEnd";

            Debug.Log("End battle");
        }
    }

    private void QueueOutput()
    {
        foreach (UnitStatus unit in battleQueue)
        {
            if (unit == null)
            {
                Debug.Log($"Null");
                return;
            }

            Debug.Log($"{unit.gameObject} - {unit.status} - {unit.turn}");
        }
    }
}
