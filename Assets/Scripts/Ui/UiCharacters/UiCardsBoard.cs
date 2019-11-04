using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.CardsBoard;
using UnityEngine;
using UnityEngine.Tilemaps;
using Logger = Tools.Logger;

namespace HexCardGame.Runtime.CardsBoard
{
    [Event]
    public interface ICreateCardBoard
    {
        void OnCreateCharacter(CardBoard character);
    }
}

namespace HexCardGame.UI
{
    public class UiCardsBoard : UiEventListener, ICreateCardBoard
    {
        [SerializeField] TileBase test;
        Tilemap TileMap { get; set; }

        void ICreateCardBoard.OnCreateCharacter(CardBoard character) => DrawCharacterUi(character);

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        [Button]
        void DrawTest() => TileMap.SetTile(new Vector3Int(2, 3, 0), test);

        void DrawCharacterUi(CardBoard character)
        {
            Logger.Log<UiCardsBoard>("Character View Created");
            TileMap.SetTile(new Vector3Int(2, 3, 0), test);
        }

        [Button]
        void ClearCharactersUi() => TileMap.ClearAllTiles();
    }
}