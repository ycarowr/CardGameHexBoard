using Tools.Patterns.Singleton;
using UnityEngine;

namespace HexCardGame.SharedData
{
    public class CardDatabase : Singleton<CardDatabase>
    {
        public const string Path = "CardDatabase";
        private CardData[] _register;

        public void Load()
        {
            _register = Resources.LoadAll<CardData>(Path);
        }

        public CardData GetData(CardId id)
        {
            foreach (var i in _register)
                if (i.Id == id)
                    return i;
            return null;
        }
    }
}