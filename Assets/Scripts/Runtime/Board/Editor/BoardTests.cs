using HexCardGame.Runtime.GameBoard;
using HexCardGame.SharedData;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class BoardTests : BaseTest, ICreateBoard
    {
        IBoard _board;
        BoardData _boardData;
        bool _isCreated;

        public void OnCreateBoard(IBoard board) => _isCreated = true;

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

        public override void Create() => _board = new Board(Parameters, Dispatcher);

        [Test]
        public void BoardCreated_Test() => Assert.IsTrue(_isCreated);

        [Test]
        public void BoardDataUndesiredPositions_Test()
        {
            foreach (var i in _boardData.UndesiredPositions)
                Assert.IsFalse(_board.Has(i.x, i.y));
        }

        [Test]
        public void BoardDataDesiredPositions_Test()
        {
            var desired = _boardData.GetDesiredPositions();
            foreach (var i in desired)
                Assert.IsTrue(_board.Has(i.x, i.y));
        }

        [Test]
        public void HasPosition_Test()
        {
            var undesired = _boardData.UndesiredPositions;
            var desired = _boardData.GetDesiredPositions();
            foreach (var i in undesired)
                Assert.IsFalse(_board.Has(i.x, i.y));
            foreach (var i in desired)
                Assert.IsTrue(_board.Has(i.x, i.y));
        }

        [Test]
        public void GetPosition_Test()
        {
            var undesired = _boardData.UndesiredPositions;
            var desired = _boardData.GetDesiredPositions();
            foreach (var i in undesired)
                Assert.IsNull(_board.Get(i.x, i.y));
            foreach (var i in desired)
            {
                var position = _board.Get(i.x, i.y);
                Assert.IsTrue(position.X == i.x);
                Assert.IsTrue(position.Y == i.y);
            }
        }

        [Test]
        public void BoardGetUnExistent_Test() => Assert.IsNull(_board.Get(-1, -1));
    }
}