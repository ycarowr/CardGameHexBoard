using UnityEngine;

namespace HexCardGame.UI
{
    public class UiAnimationTurn : UiAnimation, IStartPlayerTurn
    {
        [SerializeField] PlayerId id;

        void IStartPlayerTurn.OnStartPlayerTurn(IPlayer player)
        {
            if (player.Id == id)
                StartCoroutine(Animate());
        }
    }
}