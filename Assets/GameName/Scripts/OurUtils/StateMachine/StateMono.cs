using UnityEngine;

namespace OurUtils
{
    public abstract class StateMono : MonoBehaviour
    {
        protected StateControllerMono _stateController;

        public virtual void OnStateEnter(StateControllerMono stateController)
        {
            this._stateController = stateController;
        }

        public virtual void OnStateUpdate()
        {
        }

        public virtual void OnStateExit()
        {
        }

    }
}