namespace HexCardGame
{
    /// <summary> Bottom, where the User is always sitting. </summary>
    public class UserPlayer : AiTurn
    {
        public UserPlayer(BattleFsm.BattleFsmArguments args) : base(args)
        {
        }

        protected override PlayerId Id => PlayerId.User;
        protected override bool IsAi => GameParameters.Profiles.UserPlayer.IsAi;
        protected override bool IsUser => !GameParameters.Profiles.UserPlayer.IsAi;
    }
}