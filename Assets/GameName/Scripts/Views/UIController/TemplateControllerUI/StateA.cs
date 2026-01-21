using OurUtils;
using UnityEngine;

namespace GameName
{
    public class StateA : StateMono
    {
        public override void OnStateEnter(StateControllerMono stateController)
        {
            base.OnStateEnter(stateController);
            Debug.Log("Enter State A");
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            Debug.Log("Update State A");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exit State A");
        }

    }
}