using HexCardGame.UI;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Battlefield Zone.
    /// </summary>
    public class UiZoneBattleField : UiBaseDropZone, IOnSelectBoardPosition
    {
        public void OnSelectPosition(Vector3Int position) => CardHand?.PlaySelected();
    }
}