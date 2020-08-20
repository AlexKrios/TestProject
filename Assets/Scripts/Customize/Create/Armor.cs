using UnityEngine;

namespace Customize.Create
{    
    public class Armor : MonoBehaviour
    {
        private void Start() { }

        public void Execute(UnitTeamData member)
        {
            Create(member);
            Setup();
        }

        private void Create(UnitTeamData member) { }

        private void Setup() { }
    }
}
