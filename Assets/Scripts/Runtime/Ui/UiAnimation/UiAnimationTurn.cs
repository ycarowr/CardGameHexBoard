using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiAnimationTurn : UiAnimation, IStartPlayerTurn
    {
        [SerializeField] SeatType id;

        void IStartPlayerTurn.OnStartPlayerTurn(IPlayer player)
        {
            if (player.Seat == id)
                StartCoroutine(Animate());
        }
    }
}