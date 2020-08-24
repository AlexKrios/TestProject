using System.Collections.Generic;
using System.Linq;

namespace Battle.Units.Characters.Technician
{
    public class Technician : Unit
    {
        public override void Damage()
        {
            TargetUnit.Unit.CurrentHp += attack / CurrentUnit.AimCount;
            Manager.uiManager.HpChange(TargetUnit);

            if (TargetUnit.Unit.CurrentHp > TargetUnit.Unit.Hp)
            {
                TargetUnit.Unit.CurrentHp = TargetUnit.Unit.Hp;
            }
        }

        public override List<int> UnitTarget(string team)
        {
            var target = UnitsList
                .Where(x => x.Status == "Live" && x.Team == team && x.Unit.CurrentHp < x.Unit.Hp)
                .Select(x => x.Id)
                .ToList();

            return target;
        }

        public override void MarkedTargetAndSelf()
        {
            var currentPath = "Battle/Marks/CurrentMark";
            var targetPath = "Battle/Marks/AllyMark";

            UnitsMark.Create(CurrentUnit, currentPath);
            foreach (int index in CurrentUnit.Target)
            {
                var targetUnit = UnitsList.First(x => x.Place == index);
                UnitsMark.Create(targetUnit, targetPath);
            }
        }
    }
}