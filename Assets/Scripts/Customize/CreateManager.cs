using Customize.Create;
using Units.Objects.Unit;
using UnityEngine;

namespace Customize
{
    public class CreateManager : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        /* Sub module */
        public Unit unit;
        public Weapon weapon;
        public Armor armor;
        public Thruster thruster;

        private void Start() { }

        public void CreateUnit(UnitObject unitObject)
        {
            unit.Execute(unitObject);
            weapon.Execute(unitObject.Weapon);
            armor.Execute(unitObject.Armor);
            thruster.Execute("Test");
        }
    }
}
