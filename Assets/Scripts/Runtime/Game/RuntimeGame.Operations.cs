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
        public void DrawCardFromLibrary(PlayerId playerId) => GameMechanics.HandLibrary.DrawCard(playerId);

        public void RevealCardFromLibrary(PlayerId playerId, PositionId positionId)
            => GameMechanics.PoolLibrary.RevealCard(playerId, positionId);

        public void PlayElementAt(PlayerId playerId, CardHand card, Vector2Int position)
            => GameMechanics.HandBoard.CreateBoardElementAt(playerId, card, position);

        public void PickCardFromPosition(PlayerId playerId, PositionId positionId) =>
            GameMechanics.HandPool.PickCard(playerId, positionId);

        public void ReturnCardToPosition(PlayerId playerId, CardHand cardHand, PositionId positionId) =>
            GameMechanics.HandPool.ReturnCard(playerId, cardHand, positionId);

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