using Units.Objects.Armor;
using UnityEngine;

namespace Customize.Create
{    
    public class Armor : MonoBehaviour
    {
        private void Start() { }

        public void Execute(ArmorObject armor)
        {
            Create(armor);
            Setup();
        }

        private void Create(ArmorObject armor) { }

        private void Setup() { }
    }
}
