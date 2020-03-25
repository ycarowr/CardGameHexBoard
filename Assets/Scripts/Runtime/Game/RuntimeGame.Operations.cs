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
        public void DrawCardFromLibrary(SeatType seatType) => GameMechanics.HandLibrary.DrawCard(seatType);
        public void FreeDrawCardFromLibrary(SeatType seatType) => GameMechanics.HandLibrary.FreeDrawCard(seatType);

        public void RevealCardHigherPosition(SeatType seatType) =>
            GameMechanics.PoolLibrary.RevealCardHigherPosition(seatType);

        public void RevealCardFromLibrary(SeatType seatType, PositionId positionId)
            => GameMechanics.PoolLibrary.RevealCard(seatType, positionId);

        public void PlayElementAt(SeatType seatType, CardHand card, Vector3Int position)
            => GameMechanics.HandBoard.PlayCardAt(seatType, card, position);

        public void PickCardFromPosition(SeatType seatType, PositionId positionId) =>
            GameMechanics.HandPool.PickCard(seatType, positionId);

        public void ReturnCardToPosition(SeatType seatType, CardHand cardHand, PositionId positionId) =>
            GameMechanics.HandPool.ReturnCard(seatType, cardHand, positionId);

        public void ExecuteAiTurn(SeatType id)
        {
        }

        public void ForceWin(SeatType id)
        {
            var player = TurnLogic.GetPlayer(id);
            GameMechanics.FinishGame.Execute(player);
        }

        #endregion
    }
}