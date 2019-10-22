using System;
using Tools.Patterns.Observer;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
///     Dispatcher of the events related to the Battle FSM.
/// </summary>
[CreateAssetMenu(menuName = "GameEventsDispatcher")]
public class EventsDispatcher : ObserverAtt<EventAttribute>
{
    const string FilePath = "GameEventsDispatcher";
    public static EventsDispatcher Load() => Resources.Load<EventsDispatcher>(FilePath);
}

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public class EventAttribute : Attribute
{
}