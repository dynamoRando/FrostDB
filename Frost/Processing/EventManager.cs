﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB
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

        public EventManager()
        {
            eventDictionary = new Dictionary<string, Action<IEventArgs>>();
        }


        public void StartListening(string eventName, Action<IEventArgs> listener)
        {
            Action<IEventArgs> thisEvent;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public void StopListening(string eventName, Action<IEventArgs> listener)
        {
            Action<IEventArgs> thisEvent;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Remove event from the existing one
                thisEvent -= listener;

                //Update the Dictionary
               eventDictionary[eventName] = thisEvent;
            }
        }

        public void TriggerEvent(string eventName, IEventArgs eventParam)
        {
            Action<IEventArgs> thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                Task.Run(() => thisEvent.Invoke(eventParam));
                // OR USE  instance.eventDictionary[eventName](eventParam);
            }
        }
    }
}

    

