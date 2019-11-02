using Game.Ui;
using HexCardGame.Runtime.GameScore;
using Tools;

namespace HexCardGame.UI
{
    public class UiScore : UiEventListener, ICreateScore
    {
        public void OnCreateScore(IScore score) => Logger.Log<UiScore>("Score View Created");
    }
}