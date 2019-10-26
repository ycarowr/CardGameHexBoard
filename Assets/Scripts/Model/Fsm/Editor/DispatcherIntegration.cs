using System.Collections.Generic;
using HexCardGame;
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
            GameData.CurrentGameInstance.PreStartGame();
            Assert.IsTrue(_preStarted);
        }

        [Test]
        public void StartGame()
        {
            _started = false;
            GameData.CurrentGameInstance.StartGame();
            Assert.IsTrue(_started);
        }

        [Test]
        public void EndGameUser()
        {
            GameData.CurrentGameInstance.StartGame();
            _ended = false;
            GameData.CurrentGameInstance.ForceWin(PlayerId.User);
            Assert.IsTrue(_ended);
        }

        [Test]
        public void EndGameEnemy()
        {
            GameData.CurrentGameInstance.StartGame();
            _ended = false;
            GameData.CurrentGameInstance.ForceWin(PlayerId.Enemy);
            Assert.IsTrue(_ended);
        }

        [Test]
        public void StartTurn()
        {
            GameData.CurrentGameInstance.StartGame();
            _playerStarted = false;
            GameData.CurrentGameInstance.StartCurrentPlayerTurn();
            Assert.IsTrue(_playerStarted);
        }
        
        [Test]
        public void FinishTurn()
        {
            GameData.CurrentGameInstance.StartGame();
            GameData.CurrentGameInstance.StartCurrentPlayerTurn();
            _playerFinished = false;
            GameData.CurrentGameInstance.FinishCurrentPlayerTurn();
            Assert.IsTrue(_playerFinished);
        }

        public void OnStartGame(IPlayer p) => _started = true;

        public void OnFinishGame(IPlayer winner) => _ended = true;

        public void OnPreGameStart(List<IPlayer> players) => _preStarted = true;

        public void OnStartPlayerTurn(IPlayer player) => _playerStarted = true;

        public void OnFinishPlayerTurn(IPlayer player) => _playerFinished = true;
    }
}