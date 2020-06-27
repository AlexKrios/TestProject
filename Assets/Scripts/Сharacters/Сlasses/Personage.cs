using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class Personage : MonoBehaviour, IAttack
{        
    public UnitData data;

    [NonSerialized]
    public int hp;
    [NonSerialized]
    public int attack;
    [NonSerialized]
    public int defence;
    [NonSerialized]
    public int initiative;
    [NonSerialized]
    public string type;

    protected GameObject cam;
    protected List<UnitStatus> battleQueue;

    void Awake()
    {
        hp = data.hp;
        attack = data.attack;
        defence = data.defence;
        initiative = data.initiative;
        type = data.type;
    }

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

    public virtual void MarkedTarget(int index)
    {
        UnitStatus targetUnit = battleQueue.First(x => x.place == index);
        cam.GetComponent<BattleMark>().Create(targetUnit, "test1");
    }

    public virtual void Attack() 
    {        
        BattleManager.targetUnit.currentHp -= attack;

        if (BattleManager.targetUnit.currentHp <= 0)
        {
            BattleManager.targetUnit.status = "Dead";
        }
    }

    public virtual void Skip() { }

    public void ShowStat()
    {
        Debug.Log($"Hp: {hp}, Attack: {attack}, Defence: {defence}, Initiative: {initiative}");
        //Debug.Log($"Type: {type}");
    }

    public void Test()
    {
        GameObject bullet = Instantiate(Resources.Load("Bullet", typeof(GameObject)) as GameObject);
        bullet.gameObject.transform.position = BattleManager.currentUnit.gameObject.transform.position;
        bullet.gameObject.transform.rotation = BattleManager.currentUnit.gameObject.transform.rotation;
    }
}
