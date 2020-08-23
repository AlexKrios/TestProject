using UnityEngine;

namespace Customize
{
    public class ChangeManager : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        /* Sub module */
        public ParseUnitData parseUnitData;
        public ParseWeaponData parseWeaponData;
        public ParseArmorData parseArmorData;

        private void Start() { }
    }
}
