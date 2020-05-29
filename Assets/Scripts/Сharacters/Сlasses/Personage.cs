using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Personage : MonoBehaviour
{    
    [HideInInspector]
    public int level;

    [HideInInspector]
    public int expirence;

    [HideInInspector]
    public int currentExpirence;

    public int hp;

    public int attack;

    public int defence;

    public int initiative;

    public string type;

    protected GameObject cam;
    protected List<UnitStatus> battleQueue;

    void Start() 
    {
        cam = GameObject.Find("MainCamera");
        battleQueue = cam.GetComponent<BattleQueue>().battleQueue;
    }

    public virtual List<int> UnitTarget(string team)
    {
        List<int> target = new List<int>();

        foreach (UnitStatus unit in battleQueue)
        {
            bool isNonTarget = unit.status == "Dead" || unit.team == team;

            if (isNonTarget)
            {
                continue;
            }
            
            target.Add(unit.place);
        }

        return target;
    }

    public virtual bool CheckTarget(UnitStatus currentUnit, UnitStatus targetUnit)
    {
        bool isTarget = currentUnit.target.Contains(targetUnit.place);

        if (isTarget)
        {
            return true;
        }

        return false;
    }    

    public virtual void MarkedTarget(int index)
    {
        UnitStatus targetUnit = battleQueue.First(x => x.place == index);
        cam.GetComponent<BattleMark>().Create(targetUnit, "test1");
    }

    public virtual void Attack(UnitStatus targetUnit) 
    {        
        targetUnit.currentHp -= attack;
    }

    public virtual void Skip() { }

    public void ShowStat()
    {
        //Debug.Log($"Hp: {hp}, Attack: {attack}, Defence: {defence}, Initiative: {initiative}, AttackType: {attackType}");
        Debug.Log($"Type: {type}");
    }
}
