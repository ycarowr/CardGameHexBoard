using HexCardGame.Runtime.Game;
using NUnit.Framework;
using UnityEngine;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : ICreateCreature
    {
        MockCardData _mockCardData = new MockCardData();
        Vector2Int _v2IntPosition = new Vector2Int();
        CardHand _cardHand;
        
        public void OnCreateCreature(PlayerId id, Creature creature, Vector2Int position, CardHand card)
        {
            EventReceived = true;
            Assert.IsTrue(creature.Data == _mockCardData);
            Assert.IsTrue(position == _v2IntPosition);
            Assert.IsTrue(_cardHand == card);
        }

        [Test]
        public void CreateCreatureAt_Test()
        {
            var player = Game.TurnLogic.CurrentPlayer;
            _cardHand = new CardHand(_mockCardData);
            Game.CreateCreatureAt(player, _cardHand, _v2IntPosition);
            Assert.IsTrue(EventReceived);
        }
    }
}