using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units.Turn
{
    public class TurnPhase : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        public List<UnitStatus> UnitsList { get => Manager.unitsList; }

        private void Start() { }

        public void Turn(string type)
        {
            switch (type)
            {
                case "StartStart":
                    Manager.turnStart.OnStartExecute.Invoke();
                    break;
                case "StartEnd":
                    Manager.turnStart.OnEndExecute.Invoke();
                    break;
                case "EndStart":
                    Manager.turnEnd.OnStartExecute.Invoke();
                    break;
                case "EndEnd":
                    Manager.turnEnd.OnEndExecute.Invoke();
                    break;
            }
        }
    }
}
