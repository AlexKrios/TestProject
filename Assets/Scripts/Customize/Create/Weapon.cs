using UnityEngine;

namespace Customize.Create
{
    public class Weapon : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        private void Start() { }

        public void Execute(UnitTeamData member)
        {
            Create(member);
            Setup();
        }

        private void Create(UnitTeamData member)
        {
            string pathWeapon = $"Battle/Сharacters/Hero/Weapon/Default/Weapon";

            var weaponModel = Instantiate(Resources.Load(pathWeapon, typeof(GameObject)) as GameObject);
            weaponModel.name = "Model";
            weaponModel.transform.parent = Manager.weapon.transform;
            weaponModel.transform.localPosition = new Vector3(0, 0, 0);
            weaponModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weaponModel.transform.localScale = new Vector3(1, 1, 1);
        }

        private void Setup() { }
    }
}
