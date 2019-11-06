using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    public partial class RuntimeGame
    {
        #region Operations

        public void StartGame() => GameMechanics.StartGame.Execute();
        public void PreStartGame() => GameMechanics.PreStartGame.Execute();
        public void StartPlayerTurn() => GameMechanics.StartPlayerTurn.Execute();
        public void FinishPlayerTurn() => GameMechanics.FinishPlayerTurn.Execute();
        public void DrawCardFromLibrary(IPlayer player) => GameMechanics.HandLibrary.DrawCard(player);
        public void CreateCreatureAt(IPlayer player, CardHand card, Vector2Int position)
            => GameMechanics.HandBoard.CreateCreatureAt(player, card, position);


        public void PickCardFromPosition(IPlayer player, PoolPositionIndex positionIndex) =>
            GameMechanics.HandPool.PickCard(player, positionIndex);

        public void ReturnCardToPosition(IPlayer player, CardHand cardHand, PoolPositionIndex positionIndex) =>
            GameMechanics.HandPool.ReturnCard(player, cardHand, positionIndex);

        public void ExecuteAiTurn(PlayerId id)
        {
        }

        public void ForceWin(PlayerId id)
        {
            var player = TurnLogic.GetPlayer(id);
            GameMechanics.FinishGame.Execute(player);
        }

        #endregion
    }
}