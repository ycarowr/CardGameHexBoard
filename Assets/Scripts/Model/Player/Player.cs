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
    public class Player : IPlayer
    {
        public Player(PlayerId id, GameParametersReference gameParameters, IDispatcher dispatcher)
        {
            Id = id;
            Dispatcher = dispatcher;
            GameParameters = gameParameters;
            StartTurnMechanics = new StartTurnMechanics(this);
            FinishTurnMechanics = new FinishTurnMechanics(this);
        }

        GameParametersReference GameParameters { get; }
        IDispatcher Dispatcher { get; }
        public StartTurnMechanics StartTurnMechanics { get; }
        public FinishTurnMechanics FinishTurnMechanics { get; }
        public PlayerId Id { get; }
        public bool IsUser => Id == GameParameters.Profiles.userId;
        void IPlayer.FinishTurn() => FinishTurnMechanics.FinishTurn();
        void IPlayer.StartTurn() => StartTurnMechanics.StartTurn();

        void OnCreatePlayer() => Dispatcher.Notify<ICreatePlayer>(i => i.OnCreatePlayer(this));
    }
}