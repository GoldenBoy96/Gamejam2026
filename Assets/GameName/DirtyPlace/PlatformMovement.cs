using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Platform Config")]
    public bool moveHorizontally = true; // Chọn hướng (Ngang hoặc Dọc)
    public float speed = 2f;             // Tốc độ nhanh hay chậm
    public float distance = 3f;          // Khoảng cách di chuyển từ tâm ra 1 phía

    private Vector2 startPos;

    void Start()
    {
        // Lưu vị trí gốc của Platform
        startPos = transform.position;

        // Đảm bảo Rigidbody là Kinematic để không bị ảnh hưởng bởi trọng lực
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void FixedUpdate() // Dùng FixedUpdate để mượt mà hơn với Rigidbody
    {
        // Tính toán độ lệch dựa trên hàm Sin
        // Công thức: Vị trí gốc + (Hướng * Sin(Thời gian * Tốc độ) * Khoảng cách)
        float offset = Mathf.Sin(Time.time * speed) * distance;

        if (moveHorizontally)
        {
            rb.MovePosition(new Vector2(startPos.x + offset, startPos.y));
        }
        else
        {
            rb.MovePosition(new Vector2(startPos.x, startPos.y + offset));
        }
    }
}