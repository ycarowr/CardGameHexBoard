using HexCardGame.Runtime.Game;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : BaseTestMechanics, IFinishGame
    {
        public void OnFinishGame(IPlayer winner)
        {
            EventReceived = true;
            SeatType = winner.Id;
        }

        [Test]
        public void FinishGame_User_Test()
        {
            var id = SeatType.Bottom;
            Game.ForceWin(id);
            Assert.IsTrue(EventReceived);
            Assert.IsTrue(SeatType == id);
        }

        [Test]
        public void FinishGame_Ai_Test()
        {
            var id = SeatType.Top;
            Game.ForceWin(id);
            Assert.IsTrue(EventReceived);
            Assert.IsTrue(id == SeatType);
        }
    }
}