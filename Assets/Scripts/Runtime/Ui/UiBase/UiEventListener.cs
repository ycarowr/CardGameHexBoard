using Tools.Patterns.Observer;

namespace Game.Ui
{
    /// <summary>
    ///     Base class for all classes interested on events that happen inside the FSM.
    /// </summary>
    public abstract class UiEventListener : UiGameDataAccess, IListener
    {
        /// <summary> Reference to the observer. </summary>
        protected IDispatcher Dispatcher;

        /// <summary> Add itself as a listener. </summary>
        protected override void Awake()
        {
            base.Awake();
            Dispatcher = EventsDispatcher.Load();
            Subscribe();
        }

        void OnDestroy() => Unsubscribe();
        void Subscribe() => Dispatcher.AddListener(this);
        void Unsubscribe() => Dispatcher?.RemoveListener(this);
    }
}