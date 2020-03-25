namespace HexCardGame.Runtime.GamePool
{
    public partial class Pool<T>
    {
        #region Locking Logic

        public bool IsPositionLocked(PositionId positionId)
        {
            return Get(positionId).IsLocked;
        }

        public void Unlock(PositionId positionId)
        {
        }

        public void Lock(PositionId positionId)
        {
        }

        #endregion
    }
}