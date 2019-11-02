using System;

namespace HexCardGame.Runtime.GamePool
{
    public static class PoolPositionUtility
    {
        public static PoolPositionIndex[] GetAllIds() =>
            (PoolPositionIndex[]) Enum.GetValues(typeof(PoolPositionIndex));

        public static int[] GetAllIdsInt() => (int[]) Enum.GetValues(typeof(PoolPositionIndex));
    }
    
    public enum PoolPositionIndex
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9
    }
}