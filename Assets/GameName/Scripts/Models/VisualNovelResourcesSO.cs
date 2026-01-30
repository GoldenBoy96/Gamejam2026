using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Gamejam2026
{
    [CreateAssetMenu(fileName = "VNResources", menuName = "VisualNovel/ResourcesSO")]
    public class VisualNovelResourcesSO : ScriptableObject
    {
        [Serializable]
        public class CharacterSpriteData
        {
            public string characterId;
            public string faceId;
            public Sprite sprite;
        }

        [Serializable]
        public class BackgroundData
        {
            public string backgroundId;
            public Sprite sprite;
        }

        [SerializeField] private List<CharacterSpriteData> _characterSprites;
        [SerializeField] private List<BackgroundData> _backgrounds;

        public Sprite GetCharacterSprite(string charId, string faceId)
        {
            var data = _characterSprites.FirstOrDefault(x => x.characterId == charId && x.faceId == faceId);
            return data != null ? data.sprite : null;
        }

        public Sprite GetBackground(string bgId)
        {
            var data = _backgrounds.FirstOrDefault(x => x.backgroundId == bgId);
            return data != null ? data.sprite : null;
        }
    }
}