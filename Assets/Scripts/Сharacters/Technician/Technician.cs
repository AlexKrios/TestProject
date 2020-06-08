using System.Collections.Generic;
using System.Linq;

public class Technician : Personage
{
    public override void Attack()
    {
        BattleManager.targetUnit.currentHp += attack;

        if (BattleManager.targetUnit.currentHp > BattleManager.targetUnit.hp)
        {
            BattleManager.targetUnit.currentHp = BattleManager.targetUnit.hp;
        }
    }

    public override List<int> UnitTarget(string team)
    {
        List<int> target = new List<int>();

        foreach (UnitStatus unit in battleQueue)
        {            
            bool isNonTarget = unit.status == "Dead" || unit.team != team;

            if (isNonTarget || unit.currentHp >= unit.hp)
            {
                continue;
            }

            target.Add(unit.place);
        }

        return target;
    }

    public override void MarkedTarget(int index)
    {
        UnitStatus targetUnit = battleQueue.First(x => x.place == index);
        cam.GetComponent<BattleMark>().Create(targetUnit, "test3");
    }
}
