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
        PlayerId Id { get; }
        bool IsUser { get; }
    }

    /// <summary> A concrete player class. </summary>
    public class Player : IPlayer
    {
        public Player(PlayerId id, GameParameters gameParameters, IDispatcher dispatcher)
        {
            Id = id;
            Dispatcher = dispatcher;
            GameParameters = gameParameters;
        }
        
        public PlayerId Id { get; }
        IDispatcher Dispatcher { get; }
        GameParameters GameParameters { get; }
        public bool IsUser => Id == GameParameters.Profiles.userId;

        void OnCreatePlayer() => Dispatcher.Notify<ICreatePlayer>(i => i.OnCreatePlayer(this));
    }
}