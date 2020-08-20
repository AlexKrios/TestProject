using System.Collections.Generic;
using System.Linq;

namespace Battle.Units.Characters.Technician
{
    public class Technician : Unit
    {
        public override void Damage()
        {
            TargetUnit.currentHp += attack / CurrentUnit.aimCount;
            Manager.uiManager.HpChange(TargetUnit);

            if (TargetUnit.currentHp > TargetUnit.hp)
            {
                TargetUnit.currentHp = TargetUnit.hp;
            }
        }

        public override List<int> UnitTarget(string team)
        {
            var target = UnitsList
                .Where(x => x.status == "Live" && x.team == team && x.currentHp < x.hp)
                .Select(x => x.place)
                .ToList();

            return target;
        }

        public override void MarkedTargetAndSelf()
        {
            var currentPath = "Battle/Marks/CurrentMark";
            var targetPath = "Battle/Marks/AllyMark";

            UnitsMark.Create(CurrentUnit, currentPath);
            foreach (int index in CurrentUnit.target)
            {
                var targetUnit = UnitsList.First(x => x.place == index);
                UnitsMark.Create(targetUnit, targetPath);
            }
        }
    }
}