using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPosition : MonoBehaviour
    {
        [SerializeField] PoolPositionIndex index;
        GameObject StoredCard { get; set; }

        public void SetStoredCard(GameObject card) => StoredCard = card;
    }
}