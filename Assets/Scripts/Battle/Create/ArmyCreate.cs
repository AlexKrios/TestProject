using Battle.Units;
using Battle.Units.Characters;
using Battle.Units.Turn;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Create 
{
    public class ArmyCreate : MonoBehaviour
    {
        private int _allCount = 0;

        private void Start() { }

        public List<UnitStatus> CreateArmy(List<UnitTeamData> group)
        {
            var army = new List<UnitStatus>();

            foreach (UnitTeamData member in group)
            {
                if (member.type == "Empty")
                {
                    continue;
                }

                string path = $"Characters/{member.type}/Unit";

                UnitStatus unit = new UnitStatus();
                unit.gameObject = Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject);
                var unitClass = unit.gameObject.GetComponent<Unit>();

                unit.name = unitClass.type;
                unit.parent = GameObject.Find($"{member.team}{member.place}");
                unit.gameObject.transform.parent = unit.parent.transform;
                unit.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                unit.gameObject.tag = member.team;

                unit.armor = unit.gameObject.transform.Find("Armor").gameObject;
                unit.weapon = unit.gameObject.transform.Find("Weapon").gameObject;
                unit.face = unit.gameObject.transform.Find("Face").gameObject;

                unit.gameObject.AddComponent<Damagable>();
                unit.gameObject.AddComponent<EnemyTurn>();

                CreateWeapon(unit);
                CreateArmor(unit);

                /* Export stats */
                unit.status = "Live";
                unit.turn = true;
                unit.team = member.team;
                unit.place = _allCount;
                unit.hp = unitClass.hp;
                unit.currentHp = unitClass.hp;
                unit.attack = unitClass.attack;
                unit.attackType = unitClass.attackType;
                unit.defence = unitClass.defence;
                unit.initiative = unitClass.initiative;

                army.Add(unit);
                _allCount++;
            }

            return army;
        }

        private void CreateWeapon(UnitStatus unit)
        {
            string pathWeapon = $"Characters/Hero/Weapon/Default/Weapon";

            var weaponModel = Instantiate(Resources.Load(pathWeapon, typeof(GameObject)) as GameObject);
            weaponModel.name = "Model";
            weaponModel.transform.parent = unit.weapon.transform;
            weaponModel.transform.localPosition = new Vector3(0, 0, 0);
            weaponModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weaponModel.transform.localScale = new Vector3(1, 1, 1);

            unit.aimCount = weaponModel.GetComponent<WeaponAbstract>().aimCount;
        }

        private void CreateArmor(UnitStatus unit)
        {

        }
    }
}
