using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class IntroSlideshow : MonoBehaviour
{
    public Image displayImage; // Kéo IntroImage vào đây
    public CanvasGroup canvasGroup; // Kéo Canvas Group của IntroImage vào đây
    public List<Sprite> slides; // Danh sách các ảnh intro
    public float fadeDuration = 1.0f; // Thời gian mờ dần
    public float displayTime = 2.0f; // Thời gian ảnh đứng yên
    // public string nextSceneName; // Tên Scene tiếp theo (Menu chính)

    void Start()
    {
        // Bắt đầu bằng việc ẩn ảnh đi (Alpha = 0)
        canvasGroup.alpha = 0;
        StartCoroutine(PlaySlideshow());
    }

    IEnumerator PlaySlideshow()
    {
        foreach (Sprite slide in slides)
        {
            // 1. Đổi ảnh mới
            displayImage.sprite = slide;

            // 2. Fade In (Mờ dần vào)
            yield return StartCoroutine(Fade(0, 1));

            // 3. Chờ một khoảng thời gian
            yield return new WaitForSeconds(displayTime);

            // 4. Fade Out (Mờ dần ra)
            yield return StartCoroutine(Fade(1, 0));
        }

        // Sau khi chạy hết các slide, chuyển sang Scene tiếp theo
        // SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}