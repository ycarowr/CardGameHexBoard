using System;
using Tools.Patterns.Observer;
using UnityEngine;

/// <summary>
///     Dispatcher of the events related to the Battle FSM.
/// </summary>
[CreateAssetMenu(menuName = "References/GameEventsDispatcher")]
public class EventsDispatcherReference : ObserverAtt<EventAttribute>
{
    const string FilePath = "References/GameEventsDispatcher";
    public static EventsDispatcherReference Load() => Resources.Load<EventsDispatcherReference>(FilePath);
}

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public class EventAttribute : Attribute
{
}