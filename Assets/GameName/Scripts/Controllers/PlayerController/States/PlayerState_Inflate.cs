using UnityEngine;

namespace Gamejam2026
{
    public class PlayerState_Inflate : PlayerBaseState
    {
        public override void OnStateEnter(OurUtils.StateControllerMono controller)
        {
            base.OnStateEnter(controller);
            Debug.Log(">> INFLATE: Bay lơ lửng");

            // Tắt trọng lực để bay tự do 4 hướng
            player.Rb.gravityScale = 0;

            // Tăng Drag (lực cản) để tạo cảm giác trượt rồi dừng từ từ
            player.Rb.linearDamping = player.config.flyDrag; // Unity 6 dùng linearDamping thay cho drag
        }

        public override void OnStateUpdate()
        {
            if (player == null) return;

            // 1. XỬ LÝ DI CHUYỂN VẬT LÝ (AddForce thay vì set Velocity để có quán tính)
            Vector2 moveInput = new Vector2(player.model.inputX, player.inputY).normalized;

            // Chỉ thêm lực nếu chưa đạt tốc độ tối đa
            if (player.Rb.linearVelocity.magnitude < player.config.flyMaxSpeed)
            {
                player.Rb.AddForce(moveInput * player.config.flyAcceleration);
            }

            // 2. TÁC ĐỘNG CỦA GIÓ (Cộng thêm vector gió vào)
            if (player.windForce != Vector2.zero)
            {
                player.Rb.AddForce(player.windForce);
            }

            // 3. TRỪ STAMINA
            player.model.currentStamina -= player.config.flyStaminaCost * Time.deltaTime;

            // 4. CHUYỂN SANG GROUND POUND (Tấn công)
            // Nút J (Attack) là kích hoạt dậm xuống
            if (player.model.isAttackPressed)
            {
                player.ChangeToState(player.stateGroundPound);
                return;
            }

            // 5. THOÁT STATE
            if (!player.model.isInflateHeld || player.model.currentStamina <= 0)
            {
                player.ChangeToState(player.stateNormal);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            if (player != null)
            {
                // Trả lại vật lý bình thường
                player.Rb.gravityScale = player.DefaultGravity;
                player.Rb.linearDamping = 0; // Tắt lực cản không khí
            }
        }
    }
}