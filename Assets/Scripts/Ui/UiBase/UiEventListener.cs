using Tools.Patterns.Observer;

namespace Game.Ui
{
    /// <summary>
    ///     Base class for all classes interested on events that happen inside the FSM.
    /// </summary>
    public abstract class UiEventListener : UiGameInputRequester, IListener
    {
        /// <summary> Reference to the observer. </summary>
        IDispatcher _dispatcher;

        /// <summary> Add itself as a listener. </summary>
        protected override void Awake()
        {
            base.Awake();
            _dispatcher = EventsDispatcher.Load();
            Subscribe();
        }

        void OnDestroy() => Unsubscribe();
        void Subscribe() => _dispatcher.AddListener(this);
        void Unsubscribe() => _dispatcher?.RemoveListener(this);
    }
}