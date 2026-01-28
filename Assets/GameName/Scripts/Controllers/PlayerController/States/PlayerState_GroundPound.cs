using UnityEngine;

namespace Gamejam2026
{
    public class PlayerState_GroundPound : PlayerBaseState
    {
        public override void OnStateEnter(OurUtils.StateControllerMono controller)
        {
            base.OnStateEnter(controller);
            Debug.Log(">> GROUND POUND: Lao xuống!");

            // Reset vận tốc về 0 để dừng trượt
            player.Rb.linearVelocity = Vector2.zero;
            player.Rb.gravityScale = 0;
            player.Rb.linearDamping = 0; // Bỏ lực cản để lao cho nhanh

            // Bắn lực xuống cực mạnh
            player.Rb.AddForce(Vector2.down * player.config.groundPoundForce, ForceMode2D.Impulse);
        }

        public override void OnStateUpdate()
        {
            // Khóa hoàn toàn di chuyển ngang
            player.Rb.linearVelocity = new Vector2(0, player.Rb.linearVelocity.y);

            // Chạm đất
            if (player.IsGrounded)
            {
                ImpactDamage();
                player.ChangeToState(player.stateNormal);
            }
        }

        private void ImpactDamage()
        {
            // Logic gây damage diện rộng
            Debug.Log($"<color=red>BÙM! Gây damage bán kính {player.config.groundPoundDamageRadius}m</color>");

            // Code mẫu tìm quái (dùng sau này):
            // Collider2D[] hits = Physics2D.OverlapCircleAll(player.transform.position, player.config.groundPoundDamageRadius);
            // foreach(var hit in hits) { if(hit là quái) gây damage }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            if (player != null)
            {
                player.Rb.gravityScale = player.DefaultGravity;
            }
        }
    }
}