using HexCardGame;
using NUnit.Framework;
using Tools.Patterns.Observer;
using UnityEngine;

namespace Game.Fsm.Tests
{
    public class GameSetup : BaseBattleFsmTest
    {
        [Test]
        public void InitStateTest()
        {
            Assert.AreEqual(false, GameDataReference.CurrentGameInstance.IsGameFinished);
            Assert.AreEqual(false, GameDataReference.CurrentGameInstance.IsGameStarted);
            Assert.AreEqual(false, GameDataReference.CurrentGameInstance.IsTurnInProgress);
            Assert.AreEqual(2, GameDataReference.CurrentGameInstance.TurnLogic.Players.Length);
        }
    }
}