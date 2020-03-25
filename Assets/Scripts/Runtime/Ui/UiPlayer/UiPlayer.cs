using UnityEngine;

namespace HexCardGame.UI
{
    public interface IUiPlayer
    {
        SeatType Id { get; }
        GameData GameData { get; }
        bool IsMyTurn();
        bool IsUser();
        bool IsEnemy();
    }

    /// <summary> Main player UI. </summary>
    public class UiPlayer : MonoBehaviour, IUiPlayer
    {
        public virtual SeatType Id => SeatType.Top;
        public GameData GameData { get; private set; }

        public bool IsMyTurn()
        {
            return GameData.CurrentGameInstance.TurnLogic.IsMyTurn(Id);
        }

        public bool IsUser()
        {
            return GameData.CurrentGameInstance.TurnLogic.IsUser();
        }

        public bool IsEnemy()
        {
            return GameData.CurrentGameInstance.TurnLogic.IsEnemy();
        }

        protected virtual void Awake()
        {
            GameData = GameData.Load();
        }
    }
}