using HexCardGame.Model.Game;

namespace HexCardGame
{
    public class EnemyPlayer : EnemyTurn
    {
        public EnemyPlayer(BattleFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        protected override PlayerId Id => PlayerId.Enemy;
        protected override bool IsAi => GameParameters.Profiles.AiPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.AiPlayer.IsAi;
    }
}