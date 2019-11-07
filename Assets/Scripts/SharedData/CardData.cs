using UnityEngine;

namespace HexCardGame.SharedData
{
    public interface ICardData
    {
        CardId Id { get; }
        int Cost { get; }
        int Score { get; }
        Sprite Artwork { get; }
    }

    [CreateAssetMenu(menuName = "Data/Card")]
    public class CardData : ScriptableObject, ICardData
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