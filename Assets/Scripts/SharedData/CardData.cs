using UnityEngine;

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Data/Card")]
    public class CardData : ScriptableObject
    {
        [SerializeField] Sprite artwork;
        [Range(0, 10)] [SerializeField] int attack;
        [Range(0, 5)] [SerializeField] int cost;
        [Range(0, 10)] [SerializeField] int defense;
        [SerializeField] [Multiline] string description;
        [Range(1, 16)] [SerializeField] int health;

        // -------------------------------------------------------------------------------------------------------------

        [SerializeField] CardId id;
        [SerializeField] string nameCard;
        [SerializeField] string stringId;
        public CardId Id => id;
        public int Cost => cost;
        public int Attack => attack;
        public int Defense => defense;
        public int Health => health;

        public Sprite Artwork
        {
            get => artwork;
            set => artwork = value;
        }
    }
}