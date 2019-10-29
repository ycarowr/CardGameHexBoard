using HexCardGame.Model.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPosition : MonoBehaviour
    {
        [SerializeField] PoolPositionId id;
        GameObject StoredCard { get; set; }

        public void SetStoredCard(GameObject card) => StoredCard = card;
    }
}