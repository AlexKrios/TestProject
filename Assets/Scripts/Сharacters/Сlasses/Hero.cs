using System.Collections.Generic;
using System.Linq;

public class Hero : Personage
{
    public override bool CheckTarget(UnitStatus currentUnit, UnitStatus targetUnit)
    {
        bool isTarget = targetUnit.team == "Enemy" && currentUnit.target.Contains(targetUnit.place);

        if (isTarget)
        {
            return true;
        }

        return false;
    }

    public override List<int> UnitTarget(string team)
    {
        List<int> target = base.UnitTarget();

        bool isFrontUnitsExists = target.Any(x => new[] { 0, 1, 2 }.Any(y => y == x));

        if (isFrontUnitsExists)
        {
            target.RemoveAll(x => new[] { 3, 4, 5 }.Contains(x));
        }
        else
        {
            target.RemoveAll(x => new[] { 0, 1, 2 }.Contains(x));
        }

        return target;
    }
}
