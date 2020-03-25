using HexCardGame;
using HexCardGame.Runtime.Game;
using NUnit.Framework;

namespace Game.Fsm.Tests
{
    public class MvcIntegrationTests : BaseBattleFsmTest, IStartGame, IFinishGame, IPreGameStart,
        IStartPlayerTurn, IFinishPlayerTurn
    {
        private bool _ended;
        private bool _playerFinished;
        private bool _playerStarted;
        private bool _preStarted;
        private bool _started;

        public void OnFinishGame(IPlayer winner)
        {
            _ended = true;
        }

        public void OnFinishPlayerTurn(IPlayer player)
        {
            _playerFinished = true;
        }

        public void OnPreGameStart(IPlayer[] players)
        {
            _preStarted = true;
        }

        public void OnStartGame(IPlayer p)
        {
            _started = true;
        }

        public void OnStartPlayerTurn(IPlayer player)
        {
            _playerStarted = true;
        }

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
            GameData.CurrentGameInstance.ForceWin(SeatType.Bottom);
            Assert.IsTrue(_ended);
        }

        [Test]
        public void EndGameEnemy()
        {
            GameData.CurrentGameInstance.StartGame();
            _ended = false;
            GameData.CurrentGameInstance.ForceWin(SeatType.Top);
            Assert.IsTrue(_ended);
        }

        [Test]
        public void StartTurn()
        {
            GameData.CurrentGameInstance.StartGame();
            _playerStarted = false;
            GameData.CurrentGameInstance.StartPlayerTurn();
            Assert.IsTrue(_playerStarted);
        }

        [Test]
        public void FinishTurn()
        {
            GameData.CurrentGameInstance.StartGame();
            GameData.CurrentGameInstance.StartPlayerTurn();
            _playerFinished = false;
            GameData.CurrentGameInstance.FinishPlayerTurn();
            Assert.IsTrue(_playerFinished);
        }
    }
}