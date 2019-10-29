using System.Collections.Generic;
using HexCardGame;
using HexCardGame.Model.Game;
using NUnit.Framework;
using Tools.Patterns.Observer;
using UnityEngine;

namespace Game.Fsm.Tests
{
    public class MvcIntegrationTests : BaseBattleFsmTest, IStartGame, IFinishGame, IPreGameStart,
        IStartPlayerTurn, IFinishPlayerTurn
    {
        bool _started;
        bool _ended;
        bool _preStarted;
        bool _playerStarted;
        bool _playerFinished;

        [Test]
        public void PreStartGame()
        {
            _preStarted = false;
            GameDataReference.CurrentGameInstance.PreStartGame();
            Assert.IsTrue(_preStarted);
        }

        [Test]
        public void StartGame()
        {
            _started = false;
            GameDataReference.CurrentGameInstance.StartGame();
            Assert.IsTrue(_started);
        }

        [Test]
        public void EndGameUser()
        {
            GameDataReference.CurrentGameInstance.StartGame();
            _ended = false;
            GameDataReference.CurrentGameInstance.ForceWin(PlayerId.User);
            Assert.IsTrue(_ended);
        }

        [Test]
        public void EndGameEnemy()
        {
            GameDataReference.CurrentGameInstance.StartGame();
            _ended = false;
            GameDataReference.CurrentGameInstance.ForceWin(PlayerId.Enemy);
            Assert.IsTrue(_ended);
        }

        [Test]
        public void StartTurn()
        {
            GameDataReference.CurrentGameInstance.StartGame();
            _playerStarted = false;
            GameDataReference.CurrentGameInstance.StartCurrentPlayerTurn();
            Assert.IsTrue(_playerStarted);
        }
        
        [Test]
        public void FinishTurn()
        {
            GameDataReference.CurrentGameInstance.StartGame();
            GameDataReference.CurrentGameInstance.StartCurrentPlayerTurn();
            _playerFinished = false;
            GameDataReference.CurrentGameInstance.FinishCurrentPlayerTurn();
            Assert.IsTrue(_playerFinished);
        }

        public void OnStartGame(IPlayer p) => _started = true;

        public void OnFinishGame(IPlayer winner) => _ended = true;

        public void OnPreGameStart(IPlayer[] players) => _preStarted = true;

        public void OnStartPlayerTurn(IPlayer player) => _playerStarted = true;

        public void OnFinishPlayerTurn(IPlayer player) => _playerFinished = true;
    }
}