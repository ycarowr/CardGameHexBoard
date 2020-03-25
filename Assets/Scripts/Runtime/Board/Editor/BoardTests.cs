using HexCardGame.Runtime.GameBoard;
using HexCardGame.SharedData;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class BoardTests : BaseTest, ICreateBoard<MockBoardElement>
    {
        private IBoard<MockBoardElement> _board;
        private BoardData _boardData;
        private bool _isCreated;

        public void OnCreateBoard(IBoard<MockBoardElement> board)
        {
            _isCreated = true;
        }

        public override void Setup()
        {
            _boardData = Parameters.BoardData;
            base.Setup();
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
            _board = null;
        }

        public override void Create()
        {
            _board = new Board<MockBoardElement>(Parameters, Dispatcher);
        }

        [Test]
        public void BoardCreated_Test()
        {
            Assert.IsTrue(_isCreated);
        }

        [Test]
        public void BoardDataUndesiredPositions_Test()
        {
        }

        [Test]
        public void BoardDataDesiredPositions_Test()
        {
        }

        [Test]
        public void HasPosition_Test()
        {
        }

        [Test]
        public void GetPosition_Test()
        {
        }

        [Test]
        public void BoardGetUnExistent_Test()
        {
            Assert.IsNull(_board.GetPosition(-1, -1));
        }
    }
}