using HexCardGame.SharedData;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HexCardGame.Runtime.Test
{
    public partial class Mechanics_Test
    {
        public class MockCardData : ICardData
        {
            public CardId Id { get; }
            public int Cost { get; }
            public int Score { get; }
            public Sprite Artwork { get; }
            public Tile Tile { get; }
        }
    }
}