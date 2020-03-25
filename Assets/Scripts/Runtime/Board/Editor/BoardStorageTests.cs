using HexCardGame.Runtime.GameBoard;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class BoardStorageTests : BaseTest
    {
        private IBoard<MockBoardElement> _board;

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
        public void AddDataAt_Test()
        {
            var positions = _board.Positions;
            var testSample = new MockBoardElement[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position.Cell);
            }

            for (var i = 0; i < positions.Length; i++)
            {
                var data1 = testSample[i];
                var data2 = _board.GetDataFrom(positions[i].Cell);
                Assert.IsTrue(data1 == data2);
            }
        }

        [Test]
        public void HasDataAt_Test()
        {
            var positions = _board.Positions;
            var testSample = new MockBoardElement[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position.Cell);
            }

            foreach (var i in positions)
                Assert.IsTrue(_board.HasDataAt(i.Cell));
        }

        [Test]
        public void HasData_Test()
        {
            var positions = _board.Positions;
            var testSample = new MockBoardElement[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position.Cell);
            }

            foreach (var i in testSample)
                Assert.IsTrue(_board.HasData(i));
        }

        [Test]
        public void RemoveDataAt_Test()
        {
            var positions = _board.Positions;
            var testSample = new MockBoardElement[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position.Cell);
            }

            foreach (var i in positions)
            {
                var cell = i.Cell;
                _board.RemoveDataAt(cell);
                Assert.IsTrue(_board.GetDataFrom(cell) == null);
                Assert.IsFalse(_board.HasDataAt(cell));
            }
        }

        [Test]
        public void RemoveData_Test()
        {
            var positions = _board.Positions;
            var testSample = new MockBoardElement[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position.Cell);
            }

            for (var i = 0; i < positions.Length; i++)
            {
                var data1 = testSample[i];
                _board.RemoveData(data1);
                var data2 = _board.GetDataFrom(positions[i].Cell);
                Assert.IsTrue(data2 == null);
                Assert.IsFalse(_board.HasData(data1));
                Assert.IsFalse(_board.HasDataAt(positions[i].Cell));
            }
        }

        private MockBoardElement GetTestData()
        {
            return new MockBoardElement();
        }
    }
}