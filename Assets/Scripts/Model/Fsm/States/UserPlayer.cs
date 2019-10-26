using HexCardGame.Model.Game;

namespace HexCardGame
{
    /// <summary> Bottom, where the User is always sitting. </summary>
    public class UserPlayer : EnemyTurn
    {
        public UserPlayer(BattleFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        protected override PlayerId Id => PlayerId.User;
        protected override bool IsAi => GameParameters.Profiles.UserPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.UserPlayer.IsAi;
    }
}