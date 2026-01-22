using System;
using UnityEngine;

namespace Gamejam2026
{
    [Serializable]
    public class BaseBehaviour
    {
        protected GameObject _owner;
        protected GameObject _target;

        public void Setup(GameObject owner)
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
