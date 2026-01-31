using UnityEngine;
using Gamejam2026;
public class StoryStarter1 : MonoBehaviour
{
    void Start()
    {
        VisualNovelManager.Instance.LoadStory("story_part2.json");
    }
}