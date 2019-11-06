using HexCardGame.Runtime.Game;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : BaseTestMechanics, IStartGame
    {
        public void OnStartGame(IPlayer starter)
        {
            PlayerId = starter.Id;
            EventReceived = true;
        }

        [Test]
        public void StartGame_Test()
        {
            Game.StartGame();
            Assert.IsTrue(Game.IsGameStarted);
            Assert.IsTrue(EventReceived);
            Assert.IsTrue(PlayerId == Game.TurnLogic.CurrentPlayerId);
        }
    }
}