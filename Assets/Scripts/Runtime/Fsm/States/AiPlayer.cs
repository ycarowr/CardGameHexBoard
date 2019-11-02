namespace HexCardGame
{
    public class AiPlayer : AiTurn
    {
        public AiPlayer(BattleFsm.BattleFsmArguments args) : base(args)
        {
        }

        protected override PlayerId Id => PlayerId.Ai;
        protected override bool IsAi => GameParameters.Profiles.AiPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.AiPlayer.IsAi;
    }
}