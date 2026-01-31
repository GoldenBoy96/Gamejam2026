using UnityEngine;
using Gamejam2026;
public class StoryStarter : MonoBehaviour
{
    void Start()
    {
        VisualNovelManager.Instance.LoadStory("storyPart1.json");
    }
}