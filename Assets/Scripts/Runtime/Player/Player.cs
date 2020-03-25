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
        bool IsUser { get; }
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

        IDispatcher Dispatcher { get; }
        GameParameters GameParameters { get; }

        public int NetworkId { get; }
        public SeatType Seat { get; }
        public bool IsUser => Seat == GameParameters.Profiles.localPlayerSeat;

        void OnCreatePlayer() => Dispatcher.Notify<ICreatePlayer>(i => i.OnCreatePlayer(this));
    }
}