using HexCardGame.Model.Game;
using HexCardGame;

namespace HexCardGame
{
    /// <summary> Holds the Game flow when a match is Finished. </summary>
    public class EndBattle : BaseBattleState, IFinishGame
    {
        public EndBattle(TurnBasedFsm fsm, IGame game, GameParameters gameParameters,
            EventsDispatcher gameEvents) :
            base(fsm, game, gameParameters, gameEvents)
        {
        }

        void IFinishGame.OnFinishGame(IPlayer winner) => Fsm.EndBattle();
    }
}