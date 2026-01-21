using UnityEngine;

namespace GameName
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject _platformObject;
        [SerializeField] private BehaviourCyclicalMovement _cyclicalMovementBehaviour;

        private void Awake()
        {
            _cyclicalMovementBehaviour.SetOwner(_platformObject);
        }

        void Start()
        {
            _cyclicalMovementBehaviour.StartBehaviour();
        }

        void Update()
        {
            _cyclicalMovementBehaviour.OnUpdateBehaviour();
        }
    }
}