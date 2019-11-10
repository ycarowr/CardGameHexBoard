using UnityEngine;

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Variables/PlayerDeck")]
    public class PlayerDeck : ScriptableObject
    {
        [Tooltip("Deck of a player."), SerializeField] 
        CardData[] deck;

        public CardData[] GetDeck() => deck;
    }
}