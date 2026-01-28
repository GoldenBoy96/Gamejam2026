using OurUtils;
using UnityEngine;

namespace Gamejam2026
{
    public abstract class PlayerBaseState : StateMono
    {
        protected PlayerController player;

        public override void OnStateEnter(StateControllerMono controller)
        {
            base.OnStateEnter(controller);
            player = controller as PlayerController;
            if (player == null)
            {
                Debug.LogError("LỖI NGHIÊM TRỌNG: Không tìm thấy PlayerController! Kiểm tra lại Namespace hoặc file Script.");
            }
        }
    }
}