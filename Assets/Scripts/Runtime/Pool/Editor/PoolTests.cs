using HexCardGame.Runtime.GamePool;
using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class PoolTests : BaseTest, ICreatePool
    {
        bool _isCreated;
        IPool _pool;
        public void OnCreatePool(IPool pool) => _isCreated = true;

        public override void Create() => _pool = new Pool(Parameters, Dispatcher);

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
        public void FlipCardAt_Test()
        {
            FillPool();
            var positions = PoolPositionUtility.GetAllIndices();
            var stateBefore = new bool[_pool.Size()];
            var stateAfter = new bool[_pool.Size()];
            var count = 0;
            foreach (var i in positions)
            {
                var card = _pool.GetCardAt(i);
                stateBefore[count] = card.IsFaceUp;
                _pool.FlipCardAt(i);
                stateAfter[count] = card.IsFaceUp;
                ++count;
            }

            for (var i = 0; i < stateBefore.Length; i++)
                Assert.IsTrue(stateBefore[i] == !stateAfter[i]);
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