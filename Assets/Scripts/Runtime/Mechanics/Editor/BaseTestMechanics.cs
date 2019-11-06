using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.Runtime.Test
{
    public abstract class BaseTestMechanics : BaseTest
    {
        protected bool EventReceived;
        protected IGame Game;
        protected GameMechanics GameMechanics;
        protected PlayerId PlayerId;

        public override void Setup()
        {
            base.Setup();
            GameMechanics = new GameMechanics();
            var args = new RuntimeGame.GameArgs
            {
                Dispatcher = Dispatcher,
                GameMechanics = GameMechanics,
                GameParameters = Parameters,
                Controller = new GameObject("Controller").AddComponent<GameController>()
            };
            EventReceived = false;
            Game = new RuntimeGame(args);
            GameMechanics.Initialize(Game);
            GameMechanics.StartGame.Execute();
        }

        public override void TearDown()
        {
            base.TearDown();
            Game = null;
            GameMechanics = null;
            EventReceived = false;
        }

        public override void Create()
        {
        }
    }
}