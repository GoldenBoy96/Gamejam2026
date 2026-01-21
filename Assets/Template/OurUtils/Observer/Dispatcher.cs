using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OurUtils
{
    public class Dispatcher : SingletonMono<Dispatcher>
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

        public void RemoveListener(string name, Action<object[]> callback)
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

            foreach (var listener in Listeners[name].ToList())
            {
                try
                {
                    listener.Invoke(data);
                }
                catch
                {
                    RemoveListener(name, listener);
                }
            }
        }

    }
}