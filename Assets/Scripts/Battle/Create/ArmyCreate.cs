using Battle.Units.Characters;
using Battle.Units.Turn;
using Parsers;
using System.Collections.Generic;
using Units.Objects.BattleUnit;
using UnityEngine;

namespace Battle.Create 
{    
    public class ArmyCreate : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        private ParseBattleUnitData ParseBattleUnitData { get => Manager.parseBattleUnitData; }

        private void Start() { }

        public List<BattleUnitObject> CreateArmy(string dataPath)
        {
            var parsedArmyData = ParseBattleUnitData.Parse(dataPath);
            var army = new List<BattleUnitObject>();

            foreach (BattleUnitObject data in parsedArmyData)
            {
                if (data.Status == "Empty")
                {
                    continue;
                }

                data.UnitGO = Instantiate(Resources.Load(data.Unit.PathModel, typeof(GameObject)) as GameObject);

                data.Parent = GameObject.Find($"{data.Team}{data.Place}");
                data.UnitGO.transform.parent = data.Parent.transform;
                data.UnitGO.transform.localPosition = new Vector3(0, 0, 0);
                data.UnitGO.tag = data.Team;

                data.Armor = data.UnitGO.transform.Find("Armor").gameObject;
                data.Weapon = data.UnitGO.transform.Find("Weapon").gameObject;
                data.Face = data.UnitGO.transform.Find("Face").gameObject;

                data.UnitGO.AddComponent<Damagable>();
                data.UnitGO.AddComponent<EnemyTurn>();

                CreateWeapon(data);
                CreateArmor(data);

                army.Add(data);
            }

            return army;
        }

        private void CreateWeapon(BattleUnitObject data)
        {
            string pathWeapon = $"Characters/Hero/Weapon/Default/Weapon";

            var weaponModel = Instantiate(Resources.Load(pathWeapon, typeof(GameObject)) as GameObject);
            weaponModel.name = "Model";
            weaponModel.transform.parent = data.Weapon.transform;
            weaponModel.transform.localPosition = new Vector3(0, 0, 0);
            weaponModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weaponModel.transform.localScale = new Vector3(1, 1, 1);

            data.AimCount = weaponModel.GetComponent<WeaponAbstract>().aimCount;
        }

        private void CreateArmor(BattleUnitObject data)
        {

        }
    }
}
