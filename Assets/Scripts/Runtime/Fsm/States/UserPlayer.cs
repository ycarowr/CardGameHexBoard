namespace HexCardGame
{
    /// <summary> Bottom, where the User is always sitting. </summary>
    public class UserPlayer : AiTurn
    {
        public UserPlayer(BattleFsm.BattleFsmArguments args) : base(args)
        {
        }

        protected override SeatType Id => SeatType.Bottom;
        protected override bool IsAi => GameParameters.Profiles.localPlayer.isAi;
        protected override bool IsUser => !GameParameters.Profiles.localPlayer.isAi;
    }
}