using Units.Objects.Weapon;
using UnityEngine;

namespace Customize.Create
{
    public class Weapon : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        private void Start() { }

        public void Execute(WeaponObject weapon)
        {
            Create(weapon);
            Setup();
        }

        private void Create(WeaponObject weapon)
        {
            var weaponModel = Instantiate(Resources.Load(weapon.PathModel, typeof(GameObject)) as GameObject);
            weaponModel.name = "Model";
            weaponModel.transform.parent = Manager.weapon.transform;
            weaponModel.transform.localPosition = new Vector3(0, 0, 0);
            weaponModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weaponModel.transform.localScale = new Vector3(1, 1, 1);
        }

        private void Setup() { }
    }
}
