using UnityEngine;

namespace Customize
{
    public class Swipe : MonoBehaviour
    {
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnMouseDrag()
        {
            var x = Input.GetAxis("Mouse X") * 100 * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
        }
    }
}
