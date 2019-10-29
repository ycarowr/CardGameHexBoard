using Game.Ui;
using HexCardGame.Model.GameBoard;
using HexCardGame.Model.GameCharacters;
using HexCardGame.SharedData;
using UnityEngine;
using UnityEngine.Tilemaps;
using Logger = Tools.Logger;

namespace HexCardGame.Model.GameCharacters
{
    [Event]
    public interface ICreateCharacter
    {
        void OnCreateCharacter(ICharacter character);
    }

    public interface ICharacter
    {
        CharacterData Data { get; }
    }

}

namespace HexCardGame.SharedData
{
    [CreateAssetMenu(menuName = "Data/Character")]
    public class CharacterData : ScriptableObject
    {
        public TileBase Tile;
    }
}
namespace HexCardGame.UI
{
    public class UiCharacters : UiEventListener, ICreateCharacter
    {
        [SerializeField] TileBase test;
        Tilemap TileMap { get; set; }
        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        void ICreateCharacter.OnCreateCharacter(ICharacter character) => DrawCharacterUi(character);

        [Button]
        void DrawTest()
        {
            TileMap.SetTile(new Vector3Int(2, 3, 0), test);
        }
        
        void DrawCharacterUi(ICharacter character)
        {
            Logger.Log<UiCharacters>("Character View Created");
            TileMap.SetTile(new Vector3Int(2, 3, 0), test);
        }

        [Button]
        void ClearCharactersUi() => TileMap.ClearAllTiles();
    }
}