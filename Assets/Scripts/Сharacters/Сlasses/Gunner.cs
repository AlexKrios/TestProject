public class Gunner : Personage
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
}
