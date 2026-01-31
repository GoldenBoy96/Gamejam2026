using OurUtils;
using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class PlayerController : StateControllerMono, IReset
    {
        [Header("Config")]
        public PlayerConfigSO config;

        [Header("Runtime Data")]
        public PlayerModel model = new PlayerModel();

        [Header("Components")]
        public Rigidbody2D Rb;
        public Transform groundCheck;
        public LayerMask groundLayer;
        public float groundCheckRadius = 0.2f;

        // Các State (Khai báo để kéo thả trong Inspector)
        [Header("States")]
        public PlayerState_Normal stateNormal;
        public PlayerState_Roll stateRoll;
        public PlayerState_Inflate stateInflate;
        public PlayerState_GroundPound stateGroundPound;

        [Header("--- UI DEBUG ---")]
        public Slider staminaSlider; // Kéo Slider vào đây

        public float inputY;

        public Vector2 windForce;

        public bool IsGrounded { get; private set; }
        public float DefaultGravity { get; private set; }

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            DefaultGravity = Rb.gravityScale;
        }

        private void Start()
        {
            Reset();
        }

        private void Update()
        {
            // 1. Đọc Input (Set cứng theo yêu cầu)
            GatherInput();

            // 2. Cập nhật State hiện tại (Logic của team bạn)
            if (CurrentState != null)
            {
                CurrentState.OnStateUpdate();
            }

            // 3. Check Ground
            CheckGround();

            if (staminaSlider != null)
            {
                // Tính tỉ lệ phần trăm (Hiện tại / Tối đa)
                staminaSlider.value = model.currentStamina / config.maxStamina;
            }
        }

        private void GatherInput()
        {
            model.inputX = Input.GetAxisRaw("Horizontal");
            if (model.inputX != 0) Debug.Log("Có nhận Input: " + model.inputX);
            inputY = Input.GetAxisRaw("Vertical");
            model.isJumpPressed = Input.GetKeyDown(KeyCode.Space);
            model.isRollHeld = Input.GetKey(KeyCode.LeftShift);
            model.isInflateHeld = Input.GetKey(KeyCode.C);
            model.isAttackPressed = Input.GetKeyDown(KeyCode.J);
            model.isDownPressed = inputY < -0.5f || Input.GetKey(KeyCode.S);
        }

        private void CheckGround()
        {
            if (groundCheck)
                IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        public void ApplyWindForce(Vector2 force)
        {
            windForce = force;
        }

        public void TakeDamage(float amount)
        {
            model.currentHealth -= amount;
            Debug.Log($"Player HP: {model.currentHealth}");
            if (model.currentHealth <= 0) Respawn();
        }

        public void SetCheckpoint(Vector3 pos)
        {
            model.lastCheckpoint = pos;
            Debug.Log("Checkpoint Set!");
        }

        public void Respawn()
        {
            transform.position = model.lastCheckpoint;
            Reset();
        }

        public void Reset()
        {
            model.Reset(config, transform.position);
            Rb.linearVelocity = Vector2.zero;
            Rb.gravityScale = DefaultGravity;
            windForce = Vector2.zero;

            CurrentState = null;
            ChangeToState(stateNormal);
        }

        private void OnDrawGizmos()
        {
            if (groundCheck) Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
