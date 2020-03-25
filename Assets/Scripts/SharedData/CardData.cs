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
        [SerializeField] private Sprite artwork;
        [Range(0, 5), SerializeField] private int cost;
        [SerializeField, Multiline] private string description;
        [SerializeField] private CardId id;
        [SerializeField] private string nameCard;
        [Range(0, 10), SerializeField] private int score;
        [SerializeField] private string stringId;
        [SerializeField] private Tile tile;

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