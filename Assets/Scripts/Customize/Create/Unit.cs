using Units.Objects.Unit;
using UnityEngine;

namespace Customize.Create
{
    public class Unit : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        private void Start() { }

        public void Execute(UnitObject unit)
        {
            Create(unit);
            Setup();
        }

        private void Create(UnitObject unit)
        {
            Manager.unit = Instantiate(Resources.Load(unit.PathModel, typeof(GameObject)) as GameObject);

            Manager.unit.gameObject.name = unit.Type;
            Manager.unit.transform.parent = Manager.spawnPoint.transform;
            Manager.unit.transform.localPosition = new Vector3(0, 0, 0);

            Manager.weapon = Manager.unit.transform.Find("Weapon").gameObject;
            Manager.armor = Manager.unit.transform.Find("Armor").gameObject;
            //Manager.face = Manager.unit.transform.Find("Face").gameObject;
        }

        private void Setup() { }
    }
}
