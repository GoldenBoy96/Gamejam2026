using UnityEngine;

namespace OurUtils
{
    public class StateControllerMono : MonoBehaviour
    {
        [SerializeField] StateMono _currentState;

        public StateMono CurrentState { get => _currentState; set => _currentState = value; }

        private void Update()
        {
            _currentState.OnStateUpdate();
        }
        public void ChangeToState(StateMono newState)
        {
            if (_currentState != null)
            {
                if (CurrentState.Equals(newState))
                {
                    return;
                }
                CurrentState.OnStateExit();
            }
            CurrentState = newState;
            CurrentState.OnStateEnter(this);
        }
    }
}