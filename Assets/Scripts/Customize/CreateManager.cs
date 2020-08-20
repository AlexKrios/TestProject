using Customize.Create;
using UnityEngine;

namespace Customize
{
    public class CreateManager : MonoBehaviour
    {
        /* Sub module */
        public Body body;
        public Weapon weapon;
        public Armor armor;
        public Thruster thruster;

        private void Start() { }

        public void UnitCreate(UnitTeamData member)
        {
            body.Execute(member);
            weapon.Execute(member);
            armor.Execute(member);
            thruster.Execute(member);
        }
    }
}
