using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiParticlesEndGame : UiParticles, IFinishGame
    {
        private const float DelayToNotify = 1f;
        [SerializeField] private SeatType id;

        void IFinishGame.OnFinishGame(IPlayer winner)
        {
            if (winner.Seat == id) StartCoroutine(Play(DelayToNotify));
        }
    }
}