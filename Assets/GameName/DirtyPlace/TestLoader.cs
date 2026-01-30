using UnityEngine;
using Gamejam2026;

public class TestLoader : MonoBehaviour
{
    public void ClickToLoadStory()
    {
        VisualNovelManager.Instance.LoadStory("story_test.json");
    }
}