using UnityEngine;

namespace OurUtils
{
    public interface IUIController
    {
        public bool IgnoredBackKey { get; set; }
        public void OnStartUI() { }
        public void OnActiveUI() { }
        public void OnDeactiveUI() { }
        public void OnRemoveUI() { }
    }
}