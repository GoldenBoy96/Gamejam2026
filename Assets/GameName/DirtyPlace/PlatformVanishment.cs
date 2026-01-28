using System.Collections;
using UnityEngine;

public class PlatformVanishment : MonoBehaviour
{
    public float visibleTime = 2f; // Thời gian hiện
    public float hiddenTime = 2f;  // Thời gian ẩn

    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        StartCoroutine(ToggleCycle());
    }

    IEnumerator ToggleCycle()
    {
        while (true)
        {
            // Trạng thái Hiện
            sr.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            // Trạng thái Ẩn
            sr.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(hiddenTime);
        }
    }
}