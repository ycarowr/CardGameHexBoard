using HexCardGame.Model.Game;
using HexCardGame;

namespace HexCardGame
{
    public class EnemyPlayer : EnemyTurn
    {
        public EnemyPlayer(TurnBasedFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        protected override PlayerId Id => PlayerId.Enemy;
        protected  override bool IsAi => GameParameters.Profiles.TopPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.TopPlayer.IsAi;
    }
}