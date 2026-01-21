using OurUtils;
using System;
using UnityEngine;

namespace GameName
{
    [Serializable]
    public class TemplateModel : ICloneable<TemplateModel>
    {
        //For easier serialization, [Serializable] must be added to the class
        //and [SerializeField] must be added to each field
        //Do not inherit MonoBehaviour!!!
        [SerializeField] string _fieldOne;
        [SerializeField] string _fieldTwo;

        //Public field is banned! All fields should be encapsulated.
        //Properties should be used instead to show stack traces while debugging.
        //To prevent public modification, use "private set"
        public string FieldOne { get => _fieldOne; set => _fieldOne = value; }
        public string FieldTwo { get => _fieldTwo; private set => _fieldTwo = value; }

        public void TemplateMethod()
        {
            //This public method usually be used to verify data
        }

    }

}