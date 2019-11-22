using HexCardGame;
using UnityEngine;

namespace Game.Ui
{
    /// <summary>
    ///     Base class for all classes interested on send input to the game data.
    /// </summary>
    public abstract class UiGameDataAccess : MonoBehaviour
    {
        /// <summary> Reference to the game data. </summary>
        protected GameData GameData;

        protected virtual void Awake() => GameData = GameData.Load();

        public bool IsMyTurn(PlayerId id) => GameData.CurrentGameInstance.TurnLogic.IsMyTurn(id);
    }
}