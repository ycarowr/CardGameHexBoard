using UnityEngine;

namespace HexCardGame.UI
{
    public interface IUiPlayer
    {
        PlayerId Id { get; }
        GameData GameData { get; }
        bool IsMyTurn();
        bool IsUser();
        bool IsEnemy();
    }

    /// <summary> Main player UI. </summary>
    public class UiPlayer : MonoBehaviour, IUiPlayer
    {
        public virtual PlayerId Id => PlayerId.Ai;
        public GameData GameData { get; private set; }
        public bool IsMyTurn() => GameData.CurrentGameInstance.TurnLogic.IsMyTurn(Id);
        public bool IsUser() => GameData.CurrentGameInstance.TurnLogic.IsUser();
        public bool IsEnemy() => GameData.CurrentGameInstance.TurnLogic.IsEnemy();
        protected virtual void Awake() => GameData = GameData.Load();
    }
}