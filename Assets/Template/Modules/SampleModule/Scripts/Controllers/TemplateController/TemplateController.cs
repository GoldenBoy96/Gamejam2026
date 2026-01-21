using OurUtils;
using UnityEngine;

namespace Template
{
    public class TemplateController : StateControllerMono, IReset
    {
        //The Controller should be used to control State
        //Logic Handle should be put in each State
        [Header("Init Data")]
        [SerializeField] private TemplateModel _templateModel;

        [Header("Child Component")]
        [SerializeField] private Transform _childComponent;

        [Header("Runtime")]
        [SerializeField] private TemplateModel _runtimeModel;

        [Header("State Machine")]
        [SerializeField] private StateA _stateA = new();
        [SerializeField] private StateB _stateB = new();

        public TemplateModel Data { get => _runtimeModel; set => _runtimeModel = value; }

        public void Reset()
        {
            //TO DO: Reset controller

            //Reset runtime model to protect fixed data/scriptable object
            _runtimeModel = ((ICloneable<TemplateModel>)_templateModel).CloneSelf();

            //Assign default state
            ChangeToState(_stateA);

        }

        public void ReceiveData(TemplateModel data)
        {
            //TO DO: Do some thing with model (CRUD)
            _runtimeModel = data;
        }

        public TemplateModel SendData()
        {
            return _runtimeModel;
        }
    }
}