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
        protected BattleFsm Fsm;
        protected GameData GameData;
        protected GameParameters Parameters;

        [SetUp]
        public void Setup()
        {
            Parameters = GameParameters.Load();
            Dispatcher = EventsDispatcher.Load();
            GameData = GameData.Load();
            Dispatcher.AddListener(this);
            Controller = new GameObject("MockFsmController").AddComponent<MockFsmController>();
            Fsm = new BattleFsm(Controller, GameData.CurrentGameInstance, Parameters, Dispatcher);
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
            BattleFsm _fsm;
            public MonoBehaviour MonoBehaviour => this;

            public void RestartGameImmediately()
            {
            }
        }
    }
}