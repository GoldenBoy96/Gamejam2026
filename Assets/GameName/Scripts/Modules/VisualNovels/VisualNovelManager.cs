using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    public class VisualNovelManager : MonoBehaviour
    {
        public static readonly VisualNovelData DEFAULT_VISUAL_NOVEL_DATA = new VisualNovelData
        {
            data = new List<List<string>>()
            {
                // TO DO: comment scene dialogue
                // 1: Introduction
                new List<string>()
                {
                    "Welcome to the Game!",
                    "This is the first line of dialogue.",
                    "Enjoy your adventure!"
                },         
                // 2: Talk to character A        
                new List<string>()
                {
                    "Welcome to the Visual Novel!",
                    "This is the second line of dialogue.",
                    "Enjoy your adventure!"
                }
            }
        };
        public List<string> GetVisualNovelData()
        {
            // TO DO: Return visual novel data
            return new List<string>();
        }
    }

    [Serializable]
    public class VisualNovelData
    {
        [SerializeField] public List<List<string>> data;
    }
}