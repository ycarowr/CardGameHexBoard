using HexCardGame;
using NUnit.Framework;

namespace Game.GlobalVariables.Tests
{
    public class TestScriptableObjectReferences
    {
        [Test]
        public void LoadDispatcher() => Assert.IsTrue(EventsDispatcherReference.Load() != null);

        [Test]
        public void LoadGameData() => Assert.IsTrue(GameDataReference.Load() != null);

        [Test]
        public void LoadGameParameters() => Assert.IsTrue(GameParametersReference.Load() != null);
    }
}