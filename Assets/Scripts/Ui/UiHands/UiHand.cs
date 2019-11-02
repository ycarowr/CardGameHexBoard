using Game.Ui;
using HexCardGame.Runtime;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiHand : UiEventListener, ICreateHand
    {
        [SerializeField] PlayerId Id;

        public void OnCreateHand(IHand hand, PlayerId id)
        {
            if (Id == id)
                Logger.Log<UiHand>("Created View Hand for id: " + id);
        }
    }
}