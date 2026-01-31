using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    // Structure for game save
    [Serializable]
    public class VNSaveData
    {
        public string levelId;
        public string currentNodeId;
        public int currentScore;
    }

    // Data structure for story load from JSON
    [Serializable]
    public class LevelStoryData
    {
        public string levelId;
        public string startNodeId;
        public List<StoryNode> nodes; // Use List to load from JSON, after that Manager will transfer to Dictionary for quick query.
    }

    [Serializable]
    public class StoryNode
    {
        public string nodeId;
        public List<DialogueLine> dialogueLines;
        public List<Option> options;
        public string nextNodeId; // Use when dont have any option
   }

    [Serializable]
    public class DialogueLine
    {
        public string characterId; // "doctor", "patient", "narrator"
        public string faceId;      // "happy", "angry"
        public string backgroundId; // "school", "dungeon"
        public string text;
    }

    [Serializable]
    public class Option
    {
        public string text;
        public string nextNodeId;
        public int scoreDelta; //point when choice
    }
}