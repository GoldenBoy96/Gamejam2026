using UnityEngine;

namespace Gamejam2026
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig")]
    public class PlayerConfigSO : ScriptableObject
    {
        [Header("--- Cơ bản ---")]
        public float moveSpeed = 6f;
        public float jumpForce = 14f;

        [Header("--- Stamina System ---")]
        public float maxStamina = 100f;
        public float staminaRegen = 15f; // Hồi phục khi đứng thường

        [Header("--- Skill Lăn (Roll) ---")]
        public float rollSpeed = 12f;          // Nhanh hơn đi thường
        public float rollStaminaCost = 25f;    // Tốn stamina mỗi giây
        public float rollJumpForce = 10f;      // Nhảy thấp hơn bình thường khi đang lăn
        public float rollAttackCooldown = 0.8f;
        public float rollAttackDashForce = 20f;// Lực dash khi tấn công

        [Header("--- Skill Phồng (Bay) ---")]
        public float flyAcceleration = 30f;    // Gia tốc bay (càng lớn càng nhanh đạt tốc độ tối đa)
        public float flyMaxSpeed = 8f;         // Tốc độ bay tối đa
        public float flyDrag = 2f;             // Độ trượt/quán tính (Càng nhỏ trượt càng ghê)
        public float flyStaminaCost = 15f;

        [Header("--- Skill Dậm (Ground Pound) ---")]
        public float groundPoundForce = 40f;   // Lao xuống cực nhanh
        public float groundPoundDamageRadius = 3f;

        [Header("--- Health ---")]
        public float maxHealth = 100f;
    }
}