using UnityEngine;

namespace Gamejam2026
{
    public class PlayerState_Inflate : PlayerBaseState
    {
        public override void OnStateEnter(OurUtils.StateControllerMono controller)
        {
            base.OnStateEnter(controller);
            Debug.Log(">> INFLATE: Bay lên (Trọng lực = 0)");

            // 1. Tắt trọng lực để bay lơ lửng
            player.Rb.gravityScale = 0;

            // 2. Tăng lực cản không khí (Drag) để hãm phanh từ từ (tạo cảm giác trượt)
            // Lưu ý: Unity 6 dùng 'linearDamping', bản cũ dùng 'drag'. Tôi dùng drag cho an toàn.
            player.Rb.linearDamping = player.config.flyDrag;
        }

        public override void OnStateUpdate()
        {
            if (player == null) return;

            // 1. LẤY INPUT 4 HƯỚNG
            // inputY đã được lấy trong PlayerController (Input.GetAxisRaw("Vertical"))
            Vector2 moveDir = new Vector2(player.model.inputX, player.inputY).normalized;

            // 2. BAY CÓ QUÁN TÍNH (AddForce)
            // Chỉ đẩy thêm lực nếu chưa đạt tốc độ tối đa
            if (player.Rb.linearVelocity.magnitude < player.config.flyMaxSpeed)
            {
                player.Rb.AddForce(moveDir * player.config.flyAcceleration);
            }

            // 3. XỬ LÝ GIÓ (Nếu có)
            if (player.windForce != Vector2.zero)
            {
                player.Rb.AddForce(player.windForce);
            }

            // 4. TRỪ STAMINA
            player.model.currentStamina -= player.config.flyStaminaCost * Time.deltaTime;

            // 5. CHUYỂN SANG GROUND POUND (Tấn công)
            // Bấm J (Attack) để dậm xuống
            if (player.model.isAttackPressed)
            {
                player.ChangeToState(player.stateGroundPound);
                return;
            }

            // 6. THOÁT TRẠNG THÁI
            // Nhả phím C hoặc hết Stamina
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
                // Trả lại trọng lực và tắt lực cản khi rơi xuống
                player.Rb.gravityScale = player.DefaultGravity;
                player.Rb.linearDamping = 0;
            }
        }
    }
}