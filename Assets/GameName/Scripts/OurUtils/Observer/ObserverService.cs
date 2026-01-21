using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OurUtils
{
    public class ObserverService : SingletonMono<ObserverService>
    {
        Dictionary<string, List<Action<object[]>>> Listeners =
           new();

        public void RegisterListener(string name, Action<object[]> callback)
        {
            if (!Listeners.ContainsKey(name))
            {
                Listeners.Add(name, new List<Action<object[]>>());
            }

            if (!Listeners[name].Contains(callback))
            {
                Listeners[name].Add(callback);
            }
        }

        private void RemoveListener(string name, Action<object[]> callback)
        {
            if (!Listeners.ContainsKey(name))
            {
                return;
            }

            Listeners[name].Remove(callback);
        }

        public void Notify(string name, params object[] data)
        {
            if (!Listeners.ContainsKey(name))
            {
                return;
            }

            foreach (var callback in Listeners[name].ToList())
            {
                if (callback == null)
                {
                    RemoveListener(name, callback);
                }
                else
                {
                    callback.Invoke(data);
                }
            }
        }

    }
}