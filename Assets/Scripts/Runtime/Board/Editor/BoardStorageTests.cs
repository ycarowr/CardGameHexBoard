using HexCardGame.Runtime.GameBoard;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class BoardStorageTests : BaseTest
    {
        IBoard<TestBoardData> _board;

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
            _board = null;
        }

        public override void Create() => _board = new Board<TestBoardData>(Parameters, Dispatcher);

        [Test]
        public void AddDataAt_Test()
        {
            var positions = _board.Positions;
            var testSample = new TestBoardData[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position);
            }

            for (var i = 0; i < positions.Length; i++)
            {
                var data1 = testSample[i];
                var data2 = _board.GetDataFrom(positions[i]);
                Assert.IsTrue(data1 == data2);
            }
        }

        [Test]
        public void HasDataAt_Test()
        {
            var positions = _board.Positions;
            var testSample = new TestBoardData[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position);
            }

            foreach (var i in positions)
                Assert.IsTrue(_board.HasDataAt(i));
        }

        [Test]
        public void HasData_Test()
        {
            var positions = _board.Positions;
            var testSample = new TestBoardData[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position);
            }

            foreach (var i in testSample)
                Assert.IsTrue(_board.HasData(i));
        }

        [Test]
        public void RemoveDataAt_Test()
        {
            var positions = _board.Positions;
            var testSample = new TestBoardData[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position);
            }

            foreach (var i in positions)
            {
                _board.RemoveDataAt(i);
                Assert.IsTrue(_board.GetDataFrom(i) == null);
                Assert.IsFalse(_board.HasDataAt(i));
            }
        }

        [Test]
        public void RemoveData_Test()
        {
            var positions = _board.Positions;
            var testSample = new TestBoardData[positions.Length];
            for (var i = 0; i < testSample.Length; i++)
                testSample[i] = GetTestData();

            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                _board.AddDataAt(testSample[i], position);
            }

            for (var i = 0; i < positions.Length; i++)
            {
                var data1 = testSample[i];
                _board.RemoveData(data1);
                var data2 = _board.GetDataFrom(positions[i]);
                Assert.IsTrue(data2 == null);
                Assert.IsFalse(_board.HasData(data1));
                Assert.IsFalse(_board.HasDataAt(positions[i]));
            }
        }

        TestBoardData GetTestData() => new TestBoardData();
    }
}