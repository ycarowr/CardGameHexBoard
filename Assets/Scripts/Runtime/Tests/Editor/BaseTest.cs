using NUnit.Framework;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime.Test
{
    public abstract class BaseTest : IListener
    {
        protected readonly EventsDispatcher Dispatcher = EventsDispatcher.Load();
        protected readonly GameParameters Parameters = GameParameters.Load();

        [SetUp]
        public virtual void Setup()
        {
            Dispatcher.AddListener(this);
            Create();
        }

        [TearDown]
        public virtual void TearDown() => Dispatcher.RemoveListener(this);

        [Test]
        public abstract void Create();
    }
}