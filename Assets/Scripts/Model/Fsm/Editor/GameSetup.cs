using NUnit.Framework;

namespace Game.Fsm.Tests
{
    public class GameSetup : BaseBattleFsmTest
    {
        [Test]
        public void InitStateTest()
        {
            Assert.AreEqual(false, GameData.CurrentGameInstance.IsGameFinished);
            Assert.AreEqual(false, GameData.CurrentGameInstance.IsGameStarted);
            Assert.AreEqual(false, GameData.CurrentGameInstance.IsTurnInProgress);
            Assert.AreEqual(2, GameData.CurrentGameInstance.TurnLogic.Players.Length);
        }
    }
}