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

            var localPlayerSeat = Parameters.Profiles.localPlayer.seat;
            var remotePlayerSeat = Parameters.Profiles.remotePlayer.seat;

            var localPlayer = new Player(0, localPlayerSeat, Parameters, Dispatcher);
            var remotePlayer = new Player(1, remotePlayerSeat, Parameters, Dispatcher);
            GameData.CreateGame(Controller, localPlayer, remotePlayer);
        }

        [TearDown]
        public virtual void End()
        {
            GameData.Clear();
            Dispatcher.RemoveListener(this);
        }

        public class MockFsmController : MonoBehaviour, IGameController
        {
            private EventsDispatcher _dispatcher;
            private GameData _gameData;
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