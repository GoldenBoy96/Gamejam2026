using System;
using System.Collections;
using UnityEngine;

namespace Template
{
    [CreateAssetMenu(fileName = "Template", menuName = "ScriptableObjects/Template", order = 0)]
    [Serializable]
    public class TemplateSO : ScriptableObject
    {
        [SerializeField] TemplateModel data;

        public TemplateModel Data { get => data; private set => data = value; }
    }
}