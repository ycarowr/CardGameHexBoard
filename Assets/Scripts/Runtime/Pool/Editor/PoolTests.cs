using HexCardGame.Runtime.GamePool;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class PoolTests : BaseTest, ICreatePool<CardPool>
    {
        bool _isCreated;
        IPool<CardPool> _pool;
        public void OnCreatePool(IPool<CardPool> pool) => _isCreated = true;

        public override void Create() => _pool = new Pool<CardPool>(Parameters, Dispatcher);

        [Test]
        public void PoolCreated_Test() => Assert.IsTrue(_isCreated);

        [Test]
        public void AddCardAt_Test()
        {
            var card = GetTestCard();
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
            {
                _pool.AddCardAt(card, i);
                Assert.AreEqual(card, _pool.GetCardAt(i));
            }
        }

        [Test]
        public void RemoveCardAt_Test()
        {
            FillPool();
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
                _pool.RemoveCardAt(i);
            foreach (var i in positions)
                Assert.AreEqual(null, _pool.GetCardAt(i));
        }

        [Test]
        public void EmptyPool_Test()
        {
            FillPool();
            _pool.Empty();
            var positions = PoolPositionUtility.GetAllIndices();
            Assert.IsTrue(_pool.Size() == 0);
            foreach (var i in positions)
                Assert.AreEqual(null, _pool.GetCardAt(i));
        }

        [Test]
        public void CoverCardAt_Test()
        {
            FillPool();
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
                _pool.CoverAt(i);
            foreach (var i in positions)
                Assert.IsTrue(_pool.GetCardAt(i).IsCovered);
        }

        [Test]
        public void UncoverCardAt_Test()
        {
            FillPool();
            var positions = PoolPositionUtility.GetAllIndices();
            foreach (var i in positions)
                _pool.CoverAt(i);
            foreach (var i in positions)
                _pool.UncoverAt(i);
            foreach (var i in positions)
                Assert.IsFalse(_pool.GetCardAt(i).IsCovered);
        }


        CardPool GetTestCard() => new CardPool(null);

        void FillPool()
        {
            var card = GetTestCard();
            var positions = PoolPositionUtility.GetAllIndices();

            //fill pool
            foreach (var i in positions)
                _pool.AddCardAt(card, i);
        }
    }
}