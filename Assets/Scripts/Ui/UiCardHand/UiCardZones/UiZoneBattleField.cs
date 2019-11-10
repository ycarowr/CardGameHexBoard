using HexCardGame.UI;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Battlefield Zone.
    /// </summary>
    public class UiZoneBattleField : UiBaseDropZone, ISelectBoardPosition
    {
        public void OnSelectBoardPosition(Vector3Int position) => CardHandSelector?.PlaySelected();
    }
}