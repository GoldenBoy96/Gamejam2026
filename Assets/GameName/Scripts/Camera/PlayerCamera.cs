using UnityEngine;

namespace Assets.GameName.Scripts.Camera
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, 2, -10);
        public float smoothSpeed = 5f;

        void LateUpdate()
        {
            if (!target) return;
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        }
    }
}
