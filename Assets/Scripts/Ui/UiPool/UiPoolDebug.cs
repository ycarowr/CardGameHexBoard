using Game.Ui;
using HexCardGame.Runtime.GamePool;

namespace HexCardGame.UI
{
    public class UiPoolDebug : UiEventListener, ICreatePool
    {
        void ICreatePool.OnCreatePool(IPool pool)
        {
        }
    }
}