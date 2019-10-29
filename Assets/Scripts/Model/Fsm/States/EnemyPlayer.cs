namespace HexCardGame
{
    public class EnemyPlayer : EnemyTurn
    {
        public EnemyPlayer(BattleFsm.BattleFsmArguments args) : base(args)
        {
        }

        protected override PlayerId Id => PlayerId.Enemy;
        protected override bool IsAi => GameParameters.Profiles.AiPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.AiPlayer.IsAi;
    }
}