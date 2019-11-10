using UnityEngine;
using UnityEngine.Tilemaps;

namespace HexCardGame.SharedData
{
    public interface ICardData
    {
        CardId Id { get; }
        int Cost { get; }
        int Score { get; }
        Sprite Artwork { get; }
        Tile Tile { get; }
    }

    [CreateAssetMenu(menuName = "Data/Card")]
    public class CardData : ScriptableObject, ICardData
    {
        [SerializeField] Sprite artwork;
        [Range(0, 5), SerializeField] int cost;
        [SerializeField, Multiline] string description;
        [SerializeField] CardId id;
        [SerializeField] string nameCard;
        [Range(0, 10), SerializeField] int score;
        [SerializeField] string stringId;
        [SerializeField] Tile tile;

        // -------------------------------------------------------------------------------------------------------------

        public CardId Id => id;
        public int Cost => cost;
        public int Score => score;

        public Sprite Artwork
        {
            get => artwork;
            set => artwork = value;
        }

        public Tile Tile => tile;
    }
}