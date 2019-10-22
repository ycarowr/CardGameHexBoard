using HexCardGame.Model.Game;
using HexCardGame;

namespace HexCardGame
{
    /// <summary> Bottom, where the User is always sitting. </summary>
    public class UserPlayer : EnemyTurn
    {
        public UserPlayer(TurnBasedFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        protected  override PlayerId Id => PlayerId.User;
        protected override bool IsAi => GameParameters.Profiles.BottomPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.BottomPlayer.IsAi;
    }
}