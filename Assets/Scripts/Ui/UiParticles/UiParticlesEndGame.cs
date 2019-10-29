using HexCardGame.Model.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiParticlesEndGame : UiParticles, IFinishGame
    {
        const float DelayToNotify = 1f;
        [SerializeField] PlayerId id;

        void IFinishGame.OnFinishGame(IPlayer winner)
        {
            if (winner.Id == id) StartCoroutine(Play(DelayToNotify));
        }
    }
}