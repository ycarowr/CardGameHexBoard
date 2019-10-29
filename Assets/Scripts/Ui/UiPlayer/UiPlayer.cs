using UnityEngine;

namespace HexCardGame.UI
{
    public interface IUiPlayer
    {
        PlayerId Id { get; }
        GameDataReference GameDataReference { get; }
        bool IsMyTurn();
        bool IsUser();
        bool IsEnemy();
    }

    /// <summary> Main player UI. </summary>
    public class UiPlayer : MonoBehaviour, IUiPlayer
    {
        public virtual PlayerId Id => PlayerId.Enemy;
        public GameDataReference GameDataReference { get; private set; }
        public bool IsMyTurn() => GameDataReference.CurrentGameInstance.TurnLogic.IsMyTurn(Id);
        public bool IsUser() => GameDataReference.CurrentGameInstance.TurnLogic.IsUser();
        public bool IsEnemy() => GameDataReference.CurrentGameInstance.TurnLogic.IsEnemy();
        protected virtual void Awake() => GameDataReference = GameDataReference.Load();
    }
}