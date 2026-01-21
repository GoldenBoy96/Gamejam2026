using System;
using UnityEngine;

namespace GameName
{
    [Serializable]
    public class BaseBehaviour
    {
        protected GameObject _owner;
        protected GameObject _target;

        public void SetOwner(GameObject owner)
        {
            _owner = owner;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
        public virtual void StartBehaviour() { }
        public virtual void OnUpdateBehaviour() { }
        public virtual void StopBehaviour() { }
        public virtual void ResetBehaviour() { }

    }
}
