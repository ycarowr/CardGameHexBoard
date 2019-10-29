using HexCardGame;
using NUnit.Framework;
using Tools.Patterns.Observer;
using UnityEngine;

namespace Game.Fsm.Tests
{
    public class BaseBattleFsmTest : IListener
    {
        protected MockFsmController Controller;
        protected EventsDispatcherReference Dispatcher;
        protected GameDataReference GameDataReference;
        protected GameParametersReference Parameters;

        [SetUp]
        public virtual void Setup()
        {
            Parameters = GameParametersReference.Load();
            Dispatcher = EventsDispatcherReference.Load();
            GameDataReference = GameDataReference.Load();
            Dispatcher.AddListener(this);
            Controller = new GameObject("MockFsmController").AddComponent<MockFsmController>();
            Controller.Awake();
            GameDataReference.Initialize(Controller);
        }

        [TearDown]
        public virtual void End()
        {
            GameDataReference.Clear();
            Dispatcher.RemoveListener(this);
        }

        public class MockFsmController : MonoBehaviour, IGameController
        {
            EventsDispatcherReference _dispatcher;
            GameDataReference _gameDataReference;
            public MonoBehaviour MonoBehaviour => this;

            public void RestartGameImmediately()
            {
                _dispatcher.Notify<IRestartGame>(i => i.OnRestart());
                _gameDataReference.Clear();
            }

            public void Awake()
            {
                _gameDataReference = GameDataReference.Load();
                _dispatcher = EventsDispatcherReference.Load();
            }
        }
    }
}