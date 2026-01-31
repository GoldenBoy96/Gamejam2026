using Gamejam2026;
using UnityEngine;


public class WindZone2D : MonoBehaviour
{
    public Vector2 windDirection = new Vector2(10f, 0); // Gió thổi mạnh sang phải

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player) player.ApplyWindForce(windDirection);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player) player.ApplyWindForce(Vector2.zero); // Hết gió
        }
    }
}

