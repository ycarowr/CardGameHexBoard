using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.Runtime.Test
{
    public abstract class BaseTestMechanics : BaseTest
    {
        protected bool EventReceived;
        protected IGame Game;
        protected SeatType SeatType;

        public override void Setup()
        {
            base.Setup();
            var args = new RuntimeGame.GameArgs
            {
                Dispatcher = Dispatcher,
                GameParameters = Parameters,
                Controller = new GameObject("Controller").AddComponent<GameController>()
            };
            EventReceived = false;
            Game = new RuntimeGame(args);
            Game.StartGame();
        }

        public override void TearDown()
        {
            base.TearDown();
            Game = null;
            EventReceived = false;
        }

        public override void Create()
        {
        }
    }
}