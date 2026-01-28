using UnityEngine;

namespace Gamejam2026
{
    public class PlayerState_Roll : PlayerBaseState
    {
        private float lockedDirection; // Hướng bị khóa

        public override void OnStateEnter(OurUtils.StateControllerMono controller)
        {
            base.OnStateEnter(controller);

            // 1. Xác định hướng lăn ngay lập tức
            if (player.model.inputX != 0)
            {
                // Nếu đang bấm phím, lăn theo hướng phím
                lockedDirection = player.model.inputX > 0 ? 1 : -1;
            }
            else
            {
                // Nếu đang đứng im, lăn theo hướng mặt đang quay (localScale.x)
                lockedDirection = player.transform.localScale.x > 0 ? 1 : -1;
            }

            Debug.Log($"<color=yellow>>> BẮT ĐẦU LĂN (Hướng: {lockedDirection})</color>");
        }

        public override void OnStateUpdate()
        {
            if (player == null) return;

            // 1. DI CHUYỂN (Cưỡng ép đi theo lockedDirection)
            float currentVelocityY = player.Rb.linearVelocity.y;
            player.Rb.linearVelocity = new Vector2(lockedDirection * player.config.rollSpeed, currentVelocityY);

            // 2. NHẢY KHI LĂN
            if (player.model.isJumpPressed && player.IsGrounded)
            {
                player.Rb.linearVelocity = new Vector2(player.Rb.linearVelocity.x, player.config.rollJumpForce);
            }

            // 3. TRỪ STAMINA
            player.model.currentStamina -= player.config.rollStaminaCost * Time.deltaTime;

            // 4. CHECK DASH ATTACK (Bấm J)
            if (player.model.isAttackPressed)
            {
                if (Time.time >= player.model.lastRollAttackTime + player.config.rollAttackCooldown)
                {
                    DashAttack();
                }
            }

            // 5. THOÁT RA
            // Nếu nhả Shift HOẶC Hết Stamina
            if (!player.model.isRollHeld || player.model.currentStamina <= 0)
            {
                Debug.Log(">> DỪNG LĂN");
                player.ChangeToState(player.stateNormal);
            }
        }

        private void DashAttack()
        {
            player.model.lastRollAttackTime = Time.time;
            Debug.Log("ROLL ATTACK!");
            // Cộng thêm lực Dash vào vận tốc hiện tại
            player.Rb.AddForce(Vector2.right * lockedDirection * player.config.rollAttackDashForce, ForceMode2D.Impulse);
        }
    }
}