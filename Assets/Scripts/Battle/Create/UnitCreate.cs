using Battle.Units;
using Battle.Units.Characters;
using UnityEngine;

namespace Battle.Create
{
    public class UnitCreate : MonoBehaviour
    {        
        private void Start() { }

        public UnitStatus CreateUnit(UnitTeamData member)
        {
            string path = $"Battle/Сharacters/{member.type}/{member.type}";

            UnitStatus unit = new UnitStatus();
            unit.gameObject = Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject);
            unit.gameObject.transform.localPosition = new Vector3(0, 0, 0);

            unit.armor = unit.gameObject.transform.Find("Armor").gameObject;
            unit.weapon = unit.gameObject.transform.Find("Weapon").gameObject;
            unit.face = unit.gameObject.transform.Find("Face").gameObject;

            CreateWeapon(unit);
            CreateArmor(unit);

            return unit;
        }

        private void CreateWeapon(UnitStatus unit)
        {
            string pathWeapon = $"Battle/Сharacters/Hero/Weapon/Default/Weapon";

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
