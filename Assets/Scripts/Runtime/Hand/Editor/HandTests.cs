using NUnit.Framework;

namespace HexCardGame.Runtime.Test
{
    public class HandTests : BaseTest, ICreateHand
    {
        readonly PlayerId _id = PlayerId.User;
        IHand _hand;
        bool _handCreated;

        public void OnCreateHand(IHand hand, PlayerId id)
        {
            _handCreated = true;
            Assert.IsTrue(id == _id);
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
            _hand = null;
        }

        public override void Create() => _hand = new Hand(_id, Parameters, Dispatcher);

        [Test]
        public void HandCreated_Test() => Assert.IsTrue(_handCreated);

        [Test]
        public void HandSize_Test() => Assert.IsTrue(_hand.MaxHandSize == Parameters.Hand.MaxHandSize);

        [Test]
        public void AddCard_Test()
        {
            var countBefore = _hand.Size();
            var card = GetTestCard();
            _hand.Add(card);
            var countAfter = _hand.Size();
            Assert.IsTrue(countBefore + 1 == countAfter);
        }

        [Test]
        public void RemoveCard_Test_True()
        {
            var card1 = GetTestCard();
            _hand.Add(card1);
            Assert.IsTrue(_hand.Remove(card1));
            Assert.IsFalse(_hand.Has(card1));
        }

        [Test]
        public void RemoveCard_Test_False()
        {
            var card1 = GetTestCard();
            Assert.IsFalse(_hand.Remove(card1));
            Assert.IsFalse(_hand.Has(card1));
        }

        CardHand GetTestCard() => new CardHand(null);
    }
}