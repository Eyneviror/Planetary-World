using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public  class GlobalGameEvent { }

public static class GlobalEvents
{
    static readonly Dictionary<Type, Action<GlobalGameEvent>> s_Events = new Dictionary<Type, Action<GlobalGameEvent>>();

    static readonly Dictionary<Delegate, Action<GlobalGameEvent>> s_EventLookups =
        new Dictionary<Delegate, Action<GlobalGameEvent>>();
    //GlobalEvents.Subscribe
    public static void Subscribe<T>(Action<T> evt) where T : GlobalGameEvent
    {
        if (!s_EventLookups.ContainsKey(evt))
        {
            Action<GlobalGameEvent> newAction = (e) => evt((T)e);
            s_EventLookups[evt] = newAction;

            if (s_Events.TryGetValue(typeof(T), out Action<GlobalGameEvent> internalAction))
                s_Events[typeof(T)] = internalAction += newAction;
            else
                s_Events[typeof(T)] = newAction;
        }
    }

    public static void Unsubscribe<T>(Action<T> evt) where T : GlobalGameEvent
    {
        if (s_EventLookups.TryGetValue(evt, out var action))
        {
            if (s_Events.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    s_Events.Remove(typeof(T));
                else
                    s_Events[typeof(T)] = tempAction;
            }

            s_EventLookups.Remove(evt);
        }
    }

    public static void Rise(GlobalGameEvent evt)
    {
        if (s_Events.TryGetValue(evt.GetType(), out var action))
            action.Invoke(evt);
    }

    public static void Clear()
    {
        s_Events.Clear();
        s_EventLookups.Clear();
    }
}

namespace PlanetaryWorld.Events
{
}


