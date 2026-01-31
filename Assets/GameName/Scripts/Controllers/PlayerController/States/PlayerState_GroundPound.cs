using UnityEngine;

namespace Gamejam2026
{
    public class PlayerState_GroundPound : PlayerBaseState
    {
        public override void OnStateEnter(OurUtils.StateControllerMono controller)
        {
            base.OnStateEnter(controller);
            Debug.Log(">> GROUND POUND: Lao xuống!");

            // 1. Dừng khựng lại trên không (Xóa quán tính cũ)
            player.Rb.linearVelocity = Vector2.zero;
            player.Rb.gravityScale = 0;
            player.Rb.linearDamping = 0; // Tắt drag để lao cho nhanh

            // 2. Bắn lực xuống cực mạnh
            player.Rb.AddForce(Vector2.down * player.config.groundPoundForce, ForceMode2D.Impulse);
        }

        public override void OnStateUpdate()
        {
            if (player == null) return;

            // 1. Khóa di chuyển ngang (Chỉ cho phép lao thẳng đứng)
            player.Rb.linearVelocity = new Vector2(0, player.Rb.linearVelocity.y);

            // 2. Kiểm tra chạm đất
            if (player.IsGrounded)
            {
                DoImpactDamage();
                player.ChangeToState(player.stateNormal);
            }
        }

        private void DoImpactDamage()
        {
            // Hiệu ứng rung màn hình hay nổ sẽ gọi ở đây
            Debug.Log($"<color=red>BÙM!!! Gây sát thương bán kính {player.config.groundPoundDamageRadius}m</color>");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            if (player != null)
            {
                // Trả lại trọng lực gốc
                player.Rb.gravityScale = player.DefaultGravity;
            }
        }
    }
}