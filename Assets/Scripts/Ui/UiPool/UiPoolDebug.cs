using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GamePool;

namespace HexCardGame.UI
{
    public class UiPoolDebug : UiEventListener, ICreatePool<CardPool>
    {
        void ICreatePool<CardPool>.OnCreatePool(IPool<CardPool> pool)
        {
        }
    }
}