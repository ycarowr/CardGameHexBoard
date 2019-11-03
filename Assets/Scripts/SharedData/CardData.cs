using UnityEngine;

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Data/Card")]
    public class CardData : ScriptableObject
    {
        [SerializeField] Sprite artwork;
        [Range(0, 5)] [SerializeField] int cost;
        [SerializeField] [Multiline] string description;
        [SerializeField] CardId id;
        [SerializeField] string nameCard;
        [Range(0, 10)] [SerializeField] int score;
        [SerializeField] string stringId;

        // -------------------------------------------------------------------------------------------------------------

        public CardId Id => id;
        public int Cost => cost;
        public int Score => score;

        public Sprite Artwork
        {
            get => artwork;
            set => artwork = value;
        }
    }
}