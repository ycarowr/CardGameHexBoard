using HexCardGame.Runtime.Game;
using NUnit.Framework;
using UnityEngine;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test : ICreateBoardElement
    {
        readonly MockCardData _mockCardData = new MockCardData();
        readonly Vector2Int _v2IntPosition = new Vector2Int();
        CardHand _cardHand;

        public void OnCreateBoardElement(PlayerId id, BoardElement boardElement, Vector2Int position, CardHand card)
        {
            EventReceived = true;
            Assert.IsTrue(boardElement.Data == _mockCardData);
            Assert.IsTrue(position == _v2IntPosition);
            Assert.IsTrue(_cardHand == card);
        }

        [Test]
        public void CreateCreatureAt_Test()
        {
            var player = Game.TurnLogic.CurrentPlayer;
            _cardHand = new CardHand(_mockCardData);
            Game.PlayElementAt(player.Id, _cardHand, _v2IntPosition);
            Assert.IsTrue(EventReceived);
        }
    }
}