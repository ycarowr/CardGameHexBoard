using System;
using UnityEngine;

namespace HexCardGame.Model.GamePool
{
    public enum PoolPositionId
    {
        Zero = 0, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
    }
    
    public class Position
    {
        public static PoolPositionId[] GetAllIds() => (PoolPositionId[]) Enum.GetValues(typeof(PoolPositionId));
        public Position(PoolPositionId id) => Id = id;
        public PoolPositionId Id { get; }
        public bool HasCard => StoredCard != null;
        public object StoredCard { get; private set; }
        public void SetStoredCard(object card) => StoredCard = card;
    }
}