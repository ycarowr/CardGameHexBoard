using HexCardGame.Runtime.Game;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : BaseTestMechanics, IFinishGame
    {
        public void OnFinishGame(IPlayer winner)
        {
            EventReceived = true;
            PlayerId = winner.Id;
        }

        [Test]
        public void FinishGame_User_Test()
        {
            var id = PlayerId.User;
            Game.ForceWin(id);
            Assert.IsTrue(EventReceived);
            Assert.IsTrue(PlayerId == id);
        }

        [Test]
        public void FinishGame_Ai_Test()
        {
            var id = PlayerId.Ai;
            Game.ForceWin(id);
            Assert.IsTrue(EventReceived);
            Assert.IsTrue(id == PlayerId);
        }
    }
}