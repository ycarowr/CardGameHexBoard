namespace HexCardGame.Runtime.GamePool
{
    public abstract class Coverable
    {
        public bool IsCovered { get; private set; }
        public void Cover() => IsCovered = true;
        public void Uncover() => IsCovered = false;
    }
}