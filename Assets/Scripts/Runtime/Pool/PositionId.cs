using System;

namespace HexCardGame.Runtime.GamePool
{
    public static class PoolPositionUtility
    {
        public static PositionId[] GetAllIndices() =>
            (PositionId[]) Enum.GetValues(typeof(PositionId));

        public static int[] GetAllIndicesInt() => (int[]) Enum.GetValues(typeof(PositionId));
    }

    public enum PositionId
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