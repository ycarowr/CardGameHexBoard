using System;
using Tools.Patterns.Observer;
using UnityEngine;

/// <summary>
///     Dispatcher of the events related to the Battle FSM.
/// </summary>
[CreateAssetMenu(menuName = "References/GameEventsDispatcher")]
public class EventsDispatcher : ObserverAtt<EventAttribute>
{
    const string FilePath = "References/GameEventsDispatcher";
    public static EventsDispatcher Load() => Resources.Load<EventsDispatcher>(FilePath);
}

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public class EventAttribute : Attribute
{
}