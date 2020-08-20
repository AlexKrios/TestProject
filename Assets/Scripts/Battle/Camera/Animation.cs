using UnityEngine;

namespace Battle.Camera
{
    public class Animation : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }

        public void CameraStart()
        {
            Manager.AddBattleStatus("CameraMove");
        }

        public void CameraEnd()
        {
            Manager.RemoveBattleStatus("CameraMove");
            Manager.turnStart.OnStartExecute.Invoke();
        }
    }
}
