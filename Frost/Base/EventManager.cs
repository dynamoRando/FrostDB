using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB.Base
{
    //Re-usable structure/ Can be a class to. Add all parameters you need inside it
    public struct EventParam
    {
        public string param1;
        public int param2;
        public float param3;
        public bool param4;
    }

    public class EventManager : IEventManager
    {
        private Dictionary<string, Action<IEventArgs>> eventDictionary;
        private static EventManager eventManager;

        public static EventManager Instance
        {
            get
            {
                eventManager.Init();
                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, Action<IEventArgs>>();
            }
        }

        public static void StartListening(string eventName, Action<IEventArgs> listener)
        {
            Action<IEventArgs> thisEvent;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                Instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action<IEventArgs> listener)
        {
            if (eventManager == null) return;
            Action<IEventArgs> thisEvent;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Remove event from the existing one
                thisEvent -= listener;

                //Update the Dictionary
                Instance.eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, IEventArgs eventParam)
        {
            Action<IEventArgs> thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                Task.Run(() => thisEvent.Invoke(eventParam));
                // OR USE  instance.eventDictionary[eventName](eventParam);
            }
        }
    }
}

    

