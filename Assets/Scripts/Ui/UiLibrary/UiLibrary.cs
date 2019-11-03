using Game.Ui;
using HexCardGame.Runtime;
using Tools;

namespace HexCardGame.UI
{
    public class UiLibrary : UiEventListener, ICreateLibrary
    {
        void ICreateLibrary.OnCreateLibrary(ILibrary lib) => Logger.Log<UiLibrary>("Library View Created: " + lib.Size);
    }
}