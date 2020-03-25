using HexCardGame.Runtime.GameScore;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class ScoreTest : BaseTest, ICreateScore
    {
        bool _isCreated;
        IScore _score;
        public void OnCreateScore(IScore score) => _isCreated = true;

        public override void TearDown()
        {
            _score = null;
            base.TearDown();
        }

        public override void Create() => _score = new Score(GetPlayers(), Parameters, Dispatcher);

        [Test]
        public void ScoreCreated_Test() => Assert.IsTrue(_isCreated);

        [Test]
        public void AddScore_Test()
        {
            var userAmount = 25;
            var aiAmount = 42;
            _score.Add(SeatType.Bottom, userAmount);
            _score.Add(SeatType.Top, aiAmount);
            Assert.IsTrue(userAmount == _score.GetScoreForPlayer(SeatType.Bottom));
            Assert.IsTrue(aiAmount == _score.GetScoreForPlayer(SeatType.Top));
        }

        [Test]
        public void RemoveScore_Test()
        {
            var startAmount = 100;
            var discountUser = 52;
            var discountAi = 42;
            _score.Add(SeatType.Bottom, startAmount);
            _score.Add(SeatType.Top, startAmount);
            _score.Remove(SeatType.Bottom, discountUser);
            _score.Remove(SeatType.Top, discountAi);
            Assert.IsTrue(startAmount - discountUser == _score.GetScoreForPlayer(SeatType.Bottom));
            Assert.IsTrue(startAmount - discountAi == _score.GetScoreForPlayer(SeatType.Top));
        }

        [Test]
        public void AddScoreCorrectPlayer_Test()
        {
            var user = SeatType.Bottom;
            var ai = SeatType.Top;
            var stateBeforeUser = _score.GetScoreForPlayer(user);
            var stateBeforeAi = _score.GetScoreForPlayer(ai);

            var amount = 42;
            _score.Add(user, amount);

            var stateAfterUser = _score.GetScoreForPlayer(user);
            var stateAfterAi = _score.GetScoreForPlayer(ai);

            Assert.IsTrue(stateBeforeUser + amount == stateAfterUser);
            Assert.IsTrue(stateBeforeAi == stateAfterAi);
        }

        [Test]
        public void RemoveScoreCorrectPlayer_Test()
        {
            var user = SeatType.Bottom;
            var ai = SeatType.Top;
            var stateBeforeUser = _score.GetScoreForPlayer(user);
            var stateBeforeAi = _score.GetScoreForPlayer(ai);

            var amount = 42;
            _score.Remove(user, amount);

            var stateAfterUser = _score.GetScoreForPlayer(user);
            var stateAfterAi = _score.GetScoreForPlayer(ai);

            Assert.IsTrue(stateBeforeUser - amount == stateAfterUser);
            Assert.IsTrue(stateBeforeAi == stateAfterAi);
        }

        [Test]
        public void ClearScore_Test()
        {
            _score.Add(SeatType.Bottom, 100);
            _score.Add(SeatType.Top, 42);
            _score.Clear();
            var stateAfterUser = _score.GetScoreForPlayer(SeatType.Bottom);
            var stateAfterAi = _score.GetScoreForPlayer(SeatType.Top);
            Assert.IsTrue(stateAfterAi == 0);
            Assert.IsTrue(stateAfterUser == 0);
        }

        IPlayer[] GetPlayers() => new IPlayer[]
        {
            new Player(SeatType.Bottom, Parameters, Dispatcher),
            new Player(SeatType.Top, Parameters, Dispatcher)
        };
    }
}