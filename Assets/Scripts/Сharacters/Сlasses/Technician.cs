using System.Collections.Generic;
using System.Linq;

public class Technician : Personage
{
    public override bool CheckTarget(UnitStatus currentUnit, UnitStatus targetUnit)
    {
        bool isTarget = targetUnit.team == "Ally" && currentUnit.target.Contains(targetUnit.place);

        if (isTarget)
        {
            return true;
        }

        return false;
    }

    public override void Attack(UnitStatus targetUnit)
    {
        targetUnit.currentHp += attack;

        if (targetUnit.currentHp > targetUnit.hp)
        {
            targetUnit.currentHp = targetUnit.hp;
        }
    }

    public override List<int> UnitTarget(string team)
    {
        List<int> target = new List<int>();

        foreach (UnitStatus unit in battleQueue)
        {
            bool isNonTarget = unit.status == "Dead" || unit.team == "Enemy";

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
        UnitStatus targetUnit = battleQueue.First(x => x.place == index && x.team == "Ally");
        cam.GetComponent<BattleMark>().Create(targetUnit, "test3");
    }
}
