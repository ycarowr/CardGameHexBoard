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

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            Dispatcher.AddListener(this);
        }

        private void Unsubscribe()
        {
            Dispatcher?.RemoveListener(this);
        }
    }
}