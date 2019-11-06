using HexCardGame.Runtime.Game;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : BaseTestMechanics, IPreGameStart
    {
        public void OnPreGameStart(IPlayer[] players) => EventReceived = true;

        [Test]
        public void PreStartGame()
        {
            Game.StartGame();
            Assert.IsTrue(EventReceived);
        }
    }
}