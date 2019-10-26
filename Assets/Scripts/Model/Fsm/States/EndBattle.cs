using HexCardGame.Model.Game;

namespace HexCardGame
{
    /// <summary> Holds the Game flow when a match is Finished. </summary>
    public class EndBattle : BaseBattleState, IFinishGame
    {
        public EndBattle(BattleFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        void IFinishGame.OnFinishGame(IPlayer winner) => Fsm.EndBattle();
    }
}