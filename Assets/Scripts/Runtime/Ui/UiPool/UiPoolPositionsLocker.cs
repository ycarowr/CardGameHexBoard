using Tools.UI.Card;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositionsLocker : MonoBehaviour, IUiInputElement
    {
        [SerializeField] UiCardHandSelector cardHandSelector;
        UiPoolPositionPickSelector[] Positions { get; set; }

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

        void Awake()
        {
            Positions = GetComponentsInChildren<UiPoolPositionPickSelector>();
            Subscribe();
        }

        void OnDestroy() => Unsubscribe();

        void Subscribe()
        {
            cardHandSelector.OnCardSelected += card => Lock();
            cardHandSelector.OnCardPlayed += uiCard => Unlock();
            cardHandSelector.OnCardUnSelect += Unlock;
        }

        void Unsubscribe()
        {
            cardHandSelector.OnCardSelected -= card => Lock();
            cardHandSelector.OnCardPlayed -= uiCard => Unlock();
            cardHandSelector.OnCardUnSelect -= Unlock;
        }
    }
}