using Unity.VisualScripting;
using UnityEngine;

namespace Gamejam2026
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject _platformObject;
        [SerializeField] private BehaviourCyclicalMovement _cyclicalMovementBehaviour;

        void Start()
        {
            // Only for testing purposes, in the future this should be called by a Parent Manager
            Setup();
            StartBehaviour();
        }

        public void Setup()
        {
            _cyclicalMovementBehaviour.Setup(_platformObject);
        }

        public void StartBehaviour()
        {
            _cyclicalMovementBehaviour.StartBehaviour();
        }

       public void UpdateBehaviour()
        {
            _cyclicalMovementBehaviour.OnUpdateBehaviour();
        }
    }
}