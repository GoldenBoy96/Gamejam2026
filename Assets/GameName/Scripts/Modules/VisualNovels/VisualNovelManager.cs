using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    public class VisualNovelManager : MonoBehaviour
    {
        public List<string> GetVisualNovelData()
        {
            // TO DO: Return visual novel data
            return new List<string>();
        }
    }

    [Serializable]
    public class VisualNovelData
    {
        [SerializeField] private List<List<string>> _data;
    }
}