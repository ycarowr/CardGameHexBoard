using Tools.Patterns.Observer;
using UnityEngine;

namespace Game.Ui
{
    /// <summary>
    ///     Base class for all classes interested on events that happen inside the FSM.
    /// </summary>
    public abstract class UiEventListener : MonoBehaviour, IListener
    {
        /// <summary> Reference to the observer. </summary>
        EventsDispatcher _dispatcher;

        /// <summary> Add itself as a listener. </summary>
        protected virtual void Awake()
        {
            //loads the asset
            _dispatcher = EventsDispatcher.Load();
            Subscribe();
        }

        void OnDestroy() => Unsubscribe();
        void Subscribe() => _dispatcher.AddListener(this);
        void Unsubscribe() => _dispatcher.RemoveListener(this);
    }
}