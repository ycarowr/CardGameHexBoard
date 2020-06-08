using Tools.Patterns.Observer;

namespace HexCardGame
{
    [Event]
    public interface ICreatePlayer
    {
        void OnCreatePlayer(IPlayer player);
    }

    public interface IPlayer
    {
        int NetworkId { get; }
        SeatType Seat { get; }
        bool IsLocalPlayer { get; }
    }

    /// <summary> A concrete player class. </summary>
    public class Player : IPlayer
    {
        public Player(int networkId, SeatType seatType, GameParameters gameParameters, IDispatcher dispatcher)
        {
            NetworkId = networkId;
            Seat = seatType;
            Dispatcher = dispatcher;
            GameParameters = gameParameters;
        }

        private IDispatcher Dispatcher { get; }
        private GameParameters GameParameters { get; }

        public int NetworkId { get; }
        public SeatType Seat { get; }
        public bool IsLocalPlayer => Seat == GameParameters.Profiles.localPlayerSeat;

        private void OnCreatePlayer()
        {
            Dispatcher.Notify<ICreatePlayer>(i => i.OnCreatePlayer(this));
        }
    }
}