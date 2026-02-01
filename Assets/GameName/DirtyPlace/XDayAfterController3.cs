using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // Thêm namespace này

public class XDayLaterController3 : MonoBehaviour
{
    public TextMeshProUGUI fadeText;
    public float fadeInTime = 2f;
    public float displayTime = 3f;
    public float fadeOutTime = 2f;
    public string nextSceneName = "VisualNovelScene3";

    void Start()
    {
        if (fadeText == null)
        {
            Debug.LogError("Fade Text (TextMeshProUGUI) is not assigned!");
            return;
        }

        // Đảm bảo ban đầu chữ trong suốt
        Color currentColor = fadeText.color;
        currentColor.a = 0;
        fadeText.color = currentColor;

        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // Fade In
        yield return StartCoroutine(Fade(0, 1, fadeInTime));

        // Display
        yield return new WaitForSeconds(displayTime);

        // Fade Out
        yield return StartCoroutine(Fade(1, 0, fadeOutTime));

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next Scene Name is not specified. Not loading a new scene.");
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        Color currentColor = fadeText.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            currentColor.a = newAlpha;
            fadeText.color = currentColor;
            yield return null;
        }

        currentColor.a = endAlpha;
        fadeText.color = currentColor;
    }
}