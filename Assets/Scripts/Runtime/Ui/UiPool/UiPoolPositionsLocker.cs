using Tools.UI.Card;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositionsLocker : MonoBehaviour, IUiInputElement
    {
        [SerializeField] private UiCardHandSelector cardHandSelector;
        private UiPoolPositionPickSelector[] Positions { get; set; }

        public bool IsLocked { get; private set; }

        public void Lock()
        {
            IsLocked = true;
            foreach (var i in Positions)
                i.Lock();
        }

        public void Unlock()
        {
            IsLocked = false;
            foreach (var i in Positions)
                i.Unlock();
        }

        private void Awake()
        {
            Positions = GetComponentsInChildren<UiPoolPositionPickSelector>();
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            cardHandSelector.OnCardSelected += card => Lock();
            cardHandSelector.OnCardPlayed += uiCard => Unlock();
            cardHandSelector.OnCardUnSelect += Unlock;
        }

        private void Unsubscribe()
        {
            cardHandSelector.OnCardSelected -= card => Lock();
            cardHandSelector.OnCardPlayed -= uiCard => Unlock();
            cardHandSelector.OnCardUnSelect -= Unlock;
        }
    }
}