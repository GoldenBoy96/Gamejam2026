using OurUtils;
using UnityEngine;

namespace GameName
{
    public class StateB : StateMono
    {
        public override void OnStateEnter(StateControllerMono stateController)
        {
            base.OnStateEnter(stateController);
            Debug.Log("Enter State B");
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            Debug.Log("Update State B");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exit State B");
        }

    }
}