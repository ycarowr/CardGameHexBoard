using NUnit.Framework;
using Tools.Patterns.Observer;
using HexCardGame;
using UnityEngine;

namespace Game.Fsm.Tests
{
    public class BaseBattleFsmTest : IListener
    {
        protected GameParameters Parameters;
        protected MockFsmController Controller;
        protected EventsDispatcher Dispatcher;
        protected GameData GameData;
        protected TurnBasedFsm Fsm;

        [SetUp]
        public void Setup()
        {
            Parameters = GameParameters.Load();
            Dispatcher = EventsDispatcher.Load();
            GameData = GameData.Load();
            Dispatcher.AddListener(this);
            Controller = new GameObject("MockFsmController").AddComponent<MockFsmController>();
            Fsm = new TurnBasedFsm(Controller, GameData.CurrentGameInstance, Parameters, Dispatcher);
            Assert.IsNotNull(Controller);
        }

        [TearDown]
        public void End()
        {
            Fsm.Clear();
            Dispatcher.RemoveListener(this);
        }

        public class MockFsmController : MonoBehaviour, IGameController
        {
            TurnBasedFsm _fsm;
            public MonoBehaviour MonoBehaviour => this;
        }
    }
}