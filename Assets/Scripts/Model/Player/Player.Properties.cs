using Tools.Patterns.Observer;

namespace HexCardGame
{
    public partial class Player
    {
        #region Properties

        public PlayerId Id { get; }
        IDispatcher Dispatcher { get; }
        GameParameters GameParameters { get; }
        public bool IsUser => Id == GameParameters.Profiles.userId;

        #endregion
    }
}