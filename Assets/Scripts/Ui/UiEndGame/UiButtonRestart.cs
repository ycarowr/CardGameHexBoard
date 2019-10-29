using System.Collections;
using System.Collections.Generic;
using HexCardGame.Model.Game;
using TMPro;
using Tools.Patterns.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace HexCardGame.UI
{
    public class UiButtonRestart : UiButton,
        IListener, IPreGameStart, IFinishGame
    {
        const float DelayToShow = 3.5f;
        EventsDispatcherReference _dispatcher;
        UITextMeshImage UiButton { get; set; }

        protected override void OnSetHandler(IButtonHandler handler)
        {
            if (handler is IPressRestart restart)
                AddListener(restart.PressRestart);
        }

        IEnumerator ShowButton()
        {
            yield return new WaitForSeconds(DelayToShow);
            UiButton.Enabled = true;
        }

        public interface IPressRestart
        {
            void PressRestart();
        }

        //----------------------------------------------------------------------------------------------------------

        #region Game Events

        void IFinishGame.OnFinishGame(IPlayer winner) => StartCoroutine(ShowButton());

        void IPreGameStart.OnPreGameStart(IPlayer[] players) => UiButton.Enabled = false;

        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region Unity callbacks

        protected void Awake()
        {
            _dispatcher = EventsDispatcherReference.Load();
            UiButton = new UITextMeshImage(
                GetComponentInChildren<TMP_Text>(),
                GetComponent<Image>());
        }

        void Start() => _dispatcher.AddListener(this);

        void OnDestroy() => _dispatcher.RemoveListener(this);

        #endregion

        //----------------------------------------------------------------------------------------------------------
    }
}