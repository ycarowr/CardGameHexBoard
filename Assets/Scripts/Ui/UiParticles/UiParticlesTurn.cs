using HexCardGame.Runtime.Game;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiParticlesTurn : UiParticles, IStartPlayerTurn
    {
        [SerializeField] PlayerId id;

        void IStartPlayerTurn.OnStartPlayerTurn(IPlayer player)
        {
            if (player.Id == id)
                StartCoroutine(Play());
        }
    }
}