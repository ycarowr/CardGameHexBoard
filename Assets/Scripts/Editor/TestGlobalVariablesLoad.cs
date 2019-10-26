using HexCardGame;
using NUnit.Framework;

namespace Game.GlobalVariables.Tests
{
    public class TestGlobalVariablesLoad
    {
        [Test]
        public void LoadDispatcher() => Assert.IsTrue(EventsDispatcher.Load() != null);

        [Test]
        public void LoadGameData() => Assert.IsTrue(GameData.Load() != null);

        [Test]
        public void LoadGameParameters() => Assert.IsTrue(GameParameters.Load() != null);
    }
}