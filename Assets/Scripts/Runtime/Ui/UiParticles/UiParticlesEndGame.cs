using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiParticlesEndGame : UiParticles, IFinishGame
    {
        const float DelayToNotify = 1f;
        [SerializeField] SeatType id;

        void IFinishGame.OnFinishGame(IPlayer winner)
        {
            if (winner.Id == id) StartCoroutine(Play(DelayToNotify));
        }
    }
}