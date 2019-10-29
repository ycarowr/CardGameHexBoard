using HexCardGame.Model.Game;

namespace HexCardGame
{
    /// <summary> Holds the Game flow when a match is Finished. </summary>
    public class EndBattle : BaseBattleState, IFinishGame
    {
        public EndBattle(BattleStateArguments args) : base(args)
        {
        }

        void IFinishGame.OnFinishGame(IPlayer winner) => Fsm.EndBattle();
    }
}