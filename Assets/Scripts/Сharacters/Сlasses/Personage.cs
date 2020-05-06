using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public abstract class Personage : MonoBehaviour
{    
    [HideInInspector]
    public int level;

    [HideInInspector]
    public int expirence;

    [HideInInspector]
    public int currentExpirence;

    public int hp;
    public Text hpBar;

    public int attack;

    public string attackType;

    public int defence;

    public int initiative;

    public string type;

    private GameObject cam;
    protected List<UnitStatus> battleQueue;

    /* Submodule classes */
    protected BattleMark battleMark = new BattleMark();

    void Start() 
    {
        cam = GameObject.Find("MainCamera");
        battleQueue = cam.GetComponent<BattleQueue>().battleQueue;
    }

    public virtual bool CheckTarget(UnitStatus currentUnit, UnitStatus targetUnit)
    {
        return false;
    }

    public virtual List<int> UnitTarget(string team = "Ally")
    {
        List<int> target = new List<int>();

        foreach (UnitStatus unit in battleQueue)
        {
            bool isAlly = unit.status == "Dead" || unit.team == team;

            if (isAlly)
            {
                continue;
            }

            target.Add(unit.place);
        }

        return target;
    }

    public virtual void MarkedTarget(int index)
    {
        UnitStatus targetUnit = battleQueue.First(x => x.place == index && x.team == "Enemy");
        battleMark.Create(targetUnit, "test1");
    }

    public void Turn() 
    {
        
    }

    public virtual void Attack(UnitStatus targetUnit) {        
        targetUnit.currentHp -= attack;
    }

    public void ShowStat()
    {
        //Debug.Log($"Hp: {hp}, Attack: {attack}, Defence: {defence}, Initiative: {initiative}, AttackType: {attackType}");
        Debug.Log($"Type: {type}");
    }
}
