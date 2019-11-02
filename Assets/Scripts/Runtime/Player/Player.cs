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
        void StartTurn();
        void FinishTurn();
    }

    /// <summary> A concrete player class. </summary>
    public partial class Player : IPlayer
    {
        public Player(PlayerId id, GameParameters gameParameters, IDispatcher dispatcher)
        {
            Id = id;
            Dispatcher = dispatcher;
            GameParameters = gameParameters;
            StartTurn = new StartTurnMechanics(this);
            FinishTurn = new FinishTurnMechanics(this);
        }

        void OnCreatePlayer() => Dispatcher.Notify<ICreatePlayer>(i => i.OnCreatePlayer(this));
    }
}