using System.Collections.Generic;

namespace Battle.Units.Interfaces
{
    public interface IUnitTarget
    {
        List<int> UnitTarget(string team);
    }
}