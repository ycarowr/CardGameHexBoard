namespace HexCardGame
{
    /// <summary> A concrete player class. </summary>
    public class Player : IPlayer
    {
        public Player(PlayerId id, GameParameters gameParameters, EventsDispatcher gameEvents)
        {
            GameParameters = gameParameters;
            Id = id;
            StartTurnMechanics = new StartTurnMechanics(this);
            FinishTurnMechanics = new FinishTurnMechanics(this);
            GameEvents = gameEvents;
        }

        public EventsDispatcher GameEvents { get; }

        public StartTurnMechanics StartTurnMechanics { get; }

        public FinishTurnMechanics FinishTurnMechanics { get; }

        //----------------------------------------------------------------------------------------------------------

        public GameParameters GameParameters { get; }

        public PlayerId Id { get; }

        public bool IsUser => Id == GameParameters.Profiles.userId;

        void IPlayer.FinishTurn() => FinishTurnMechanics.FinishTurn();

        void IPlayer.StartTurn() => StartTurnMechanics.StartTurn();
    }
}