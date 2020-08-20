using System.Collections.Generic;

namespace Battle.Units
{
    public interface IUnitTarget
    {
        List<int> UnitTarget(string team);
    }
}