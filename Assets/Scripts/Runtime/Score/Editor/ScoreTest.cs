using System.Collections;
using System.Collections.Generic;
using HexCardGame.Runtime.GameScore;
using NUnit.Framework;
using UnityEngine;


namespace HexCardGame.Runtime.Test
{
    public class ScoreTest : BaseTest, ICreateScore
    {
        bool _isCreated;
        IScore _score;

        public override void TearDown()
        {
            _score = null;
            base.TearDown();
        }

        public override void Create() => _score = new Score(GetPlayers(), Parameters, Dispatcher);
        public void OnCreateScore(IScore score) => _isCreated = true;

        [Test]
        public void ScoreCreated_Test() => Assert.IsTrue(_isCreated);

        [Test]
        public void AddScore_Test()
        {
            var userAmount = 25;
            var aiAmount = 42;
            _score.Add(PlayerId.User, userAmount);
            _score.Add(PlayerId.Ai, aiAmount);
            Assert.IsTrue(userAmount == _score.GetScoreForPlayer(PlayerId.User));
            Assert.IsTrue(aiAmount == _score.GetScoreForPlayer(PlayerId.Ai));
        }

        [Test]
        public void RemoveScore_Test()
        {
            var startAmount = 100;
            var discountUser = 52;
            var discountAi = 42;
            _score.Add(PlayerId.User, startAmount);
            _score.Add(PlayerId.Ai, startAmount);
            _score.Remove(PlayerId.User, discountUser);
            _score.Remove(PlayerId.Ai, discountAi);
            Assert.IsTrue(startAmount - discountUser == _score.GetScoreForPlayer(PlayerId.User));
            Assert.IsTrue(startAmount - discountAi == _score.GetScoreForPlayer(PlayerId.Ai));            
        }
        
        [Test]
        public void AddScoreCorrectPlayer_Test()
        {
            var user = PlayerId.User;
            var ai = PlayerId.Ai;
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
            var user = PlayerId.User;
            var ai = PlayerId.Ai;
            var stateBeforeUser = _score.GetScoreForPlayer(user);
            var stateBeforeAi = _score.GetScoreForPlayer(ai);
            
            var amount = 42;
            _score.Remove(user, amount);

            var stateAfterUser = _score.GetScoreForPlayer(user);
            var stateAfterAi = _score.GetScoreForPlayer(ai);
            
            Assert.IsTrue(stateBeforeUser - amount == stateAfterUser);
            Assert.IsTrue(stateBeforeAi == stateAfterAi);
        }
        
        IPlayer[] GetPlayers() => new IPlayer[]
        {
            new Player(PlayerId.User, Parameters, Dispatcher),
            new Player(PlayerId.Ai, Parameters, Dispatcher)
        };
    }
}
