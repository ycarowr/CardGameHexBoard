using HexCardGame.Runtime.Game;
using NUnit.Framework;
using UnityEngine;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : ICreateBoardElement
    {
        readonly MockCardData _mockCardData = new MockCardData();
        readonly Vector3Int _v3IntPosition = new Vector3Int();
        CardHand _cardHand;

        public void OnCreateBoardElement(SeatType id, CreatureElement creatureElement, Vector3Int position,
            CardHand card)
        {
            EventReceived = true;
            Assert.IsTrue(creatureElement.Data == _mockCardData);
            Assert.IsTrue(position == _v3IntPosition);
            Assert.IsTrue(_cardHand == card);
        }

        [Test]
        public void CreateCreatureAt_Test()
        {
            var player = Game.TurnLogic.CurrentPlayer;
            _cardHand = new CardHand(_mockCardData);
            Game.PlayElementAt(player.Id, _cardHand, _v3IntPosition);
            Assert.IsTrue(EventReceived);
        }
    }
}