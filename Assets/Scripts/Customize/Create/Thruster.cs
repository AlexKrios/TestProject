using UnityEngine;

namespace Customize.Create
{
    public class Thruster : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        private void Start() { }

        public void Execute(string thruster)
        {
            Create(thruster);
            Setup();
        }

        private void Create(string thruster) { }

        private void Setup() 
        {
            var trusterRight = Manager.unit.transform.Find("Trusters/MainRight").gameObject.GetComponent<ParticleSystem>().main;
            var trusterLeft = Manager.unit.transform.Find("Trusters/MainLeft").gameObject.GetComponent<ParticleSystem>().main;

            trusterRight.simulationSpace = ParticleSystemSimulationSpace.Local;
            trusterLeft.simulationSpace = ParticleSystemSimulationSpace.Local;
        }
    }
}
