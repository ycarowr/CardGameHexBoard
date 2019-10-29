using HexCardGame;
using NUnit.Framework;
using Tools.Patterns.Observer;
using UnityEngine;

namespace Game.Fsm.Tests
{
    public class BaseBattleFsmTest : IListener
    {
        protected MockFsmController Controller;
        protected EventsDispatcher Dispatcher;
        protected GameData GameData;
        protected GameParameters Parameters;

        [SetUp]
        public virtual void Setup()
        {
            Parameters = GameParameters.Load();
            Dispatcher = EventsDispatcher.Load();
            GameData = GameData.Load();
            Dispatcher.AddListener(this);
            Controller = new GameObject("MockFsmController").AddComponent<MockFsmController>();
            Controller.Awake();
            GameData.Initialize(Controller);
        }

        [TearDown]
        public virtual void End()
        {
            GameData.Clear();
            Dispatcher.RemoveListener(this);
        }

        public class MockFsmController : MonoBehaviour, IGameController
        {
            EventsDispatcher _dispatcher;
            GameData _gameData;
            public MonoBehaviour MonoBehaviour => this;

            public void RestartGameImmediately()
            {
                _dispatcher.Notify<IRestartGame>(i => i.OnRestart());
                _gameData.Clear();
            }

            public void Awake()
            {
                _gameData = GameData.Load();
                _dispatcher = EventsDispatcher.Load();
            }
        }
    }
}