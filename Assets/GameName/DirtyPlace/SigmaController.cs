using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SigmaController : MonoBehaviour
{
    public Image tayImage;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return StartCoroutine(MoveImage(new Vector2(-17, -319), 1.0f));
        yield return StartCoroutine(MoveImage(new Vector2(-17, -319), 1.0f));

        yield return StartCoroutine(MoveImage(new Vector2(-198, -272), 1.4f));

        yield return StartCoroutine(MoveImage(new Vector2(-82, -411), 2.4f));
        yield return new WaitForSeconds(1.0f);

        Debug.Log("Đã di chuyển xong tất cả các bước!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("VisualNovelScene1");

    }

    IEnumerator MoveImage(Vector2 targetPosition, float duration)
    {
        // Lấy RectTransform của Image
        RectTransform rectTransform = tayImage.rectTransform;
        Vector2 startPosition = rectTransform.anchoredPosition;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            // Tính toán vị trí mới dựa trên tỉ lệ thời gian đã trôi qua
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // Đợi khung hình tiếp theo
        }

        // Đảm bảo ảnh đến đúng vị trí cuối cùng
        rectTransform.anchoredPosition = targetPosition;
    }
}