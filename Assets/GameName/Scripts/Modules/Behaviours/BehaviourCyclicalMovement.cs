using System;
using UnityEngine;

namespace Gamejam2026
{
    [Serializable]
    public class BehaviourCyclicalMovement : BaseBehaviour
    {
        protected Vector3 _originalPosition; // Set by get the owner's position when start
        [SerializeField] WaypointData[] _waypointData = new WaypointData[0];

        public BehaviourCyclicalMovement() : base()
        {
        }

        public override void StartBehaviour()
        {
            base.StartBehaviour();
            _originalPosition = _owner.transform.position;
            _lastPosition = _originalPosition;
            for (int i = 0; i < _waypointData.Length; i++)
            {
                if (_waypointData[i].waypointObjects != null)
                {
                    _waypointData[i].Waypoint = _waypointData[i].waypointObjects.transform.position;
                }
                else
                {
                    Debug.LogWarning("BehaviourCyclicalMovement: Waypoint Object is null at index: " + i);
                }
            }
      
        }

        private float _currentWaypointIndex = 0;
        private float _elapsedTime = 0f;

        private Vector3 _lastPosition;
        public override void OnUpdateBehaviour()
        {
            base.OnUpdateBehaviour();
            if (_waypointData.Length == 0) return;

            int currentIndex = (int)_currentWaypointIndex % _waypointData.Length;
            WaypointData currentWaypoint = _waypointData[currentIndex];

            _elapsedTime += Time.deltaTime;
            float moveProgress = Mathf.Clamp01(_elapsedTime / currentWaypoint.moveToWaypointTimes);

            Vector3 targetPosition = currentWaypoint.Waypoint;
            _owner.transform.position = Vector3.Lerp(
                _lastPosition,
                targetPosition,
                moveProgress);

            if (moveProgress >= 1f)
            {
                _elapsedTime = 0;
                _currentWaypointIndex++;
                _lastPosition = _waypointData[currentIndex].Waypoint;
            }
        }

        public override void ResetBehaviour()
        {
            base.ResetBehaviour();
            _owner.transform.position = _originalPosition;
        }

    }

    [Serializable]
    class WaypointData
    {
        [SerializeField] public GameObject waypointObjects;
        public Vector3 Waypoint { get; set; } = Vector3.zero;
        [SerializeField] public float moveToWaypointTimes = 0;
    }
}
