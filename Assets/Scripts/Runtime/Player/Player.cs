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
        SeatType Id { get; }
        bool IsUser { get; }
    }

    /// <summary> A concrete player class. </summary>
    public class Player : IPlayer
    {
        public Player(SeatType id, GameParameters gameParameters, IDispatcher dispatcher)
        {
            Id = id;
            Dispatcher = dispatcher;
            GameParameters = gameParameters;
        }

        IDispatcher Dispatcher { get; }
        GameParameters GameParameters { get; }

        public SeatType Id { get; }
        public bool IsUser => Id == GameParameters.Profiles.userId;

        void OnCreatePlayer() => Dispatcher.Notify<ICreatePlayer>(i => i.OnCreatePlayer(this));
    }
}