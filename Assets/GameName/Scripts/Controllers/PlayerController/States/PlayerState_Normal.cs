using UnityEngine;

namespace Gamejam2026
{
    public class PlayerState_Normal : PlayerBaseState
    {
        public override void OnStateUpdate()
        {
            if (player == null) return;

            // --- CODE DI CHUYỂN CŨ (GIỮ NGUYÊN) ---
            float xInput = player.model.inputX;
            if (xInput != 0)
            {
                Vector3 scale = player.transform.localScale;
                scale.x = xInput > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                player.transform.localScale = scale;
            }
            player.Rb.linearVelocity = new Vector2(xInput * player.config.moveSpeed, player.Rb.linearVelocity.y);

            // Hồi stamina
            player.model.currentStamina += player.config.staminaRegen * Time.deltaTime;
            if (player.model.currentStamina > player.config.maxStamina) player.model.currentStamina = player.config.maxStamina;


            // --- MÁY DÒ LỖI (CHỈ CẦN THÊM ĐOẠN NÀY) ---
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log($"[DEBUG] Bấm Shift! Kiểm tra điều kiện:" +
                          $"\n - Chạm đất (IsGrounded): {player.IsGrounded}" +
                          $"\n - Stamina: {player.model.currentStamina}" +
                          $"\n - Config Cost: {player.config.rollStaminaCost}");
            }
            // ------------------------------------------


            // --- LOGIC CHUYỂN STATE ---
            if (player.IsGrounded && player.model.isRollHeld && player.model.currentStamina > 5)
            {
                player.ChangeToState(player.stateRoll);
                return;
            }
            // ----------------------------------------------

            // Chuyển sang Bay
            if (player.model.isInflateHeld && player.model.currentStamina > 5)
            {
                player.ChangeToState(player.stateInflate);
            }
        }
    }
}