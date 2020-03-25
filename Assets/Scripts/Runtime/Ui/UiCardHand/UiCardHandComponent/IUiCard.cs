using Game.Ui;
using HexCardGame;
using HexCardGame.SharedData;
using Tools.Patterns.StateMachine;

namespace Tools.UI.Card
{
    public interface IUiCard : IStateMachineHandler, IUiCardComponents, IUiCardTransform
    {
        bool IsDragging { get; }
        bool IsHovering { get; }
        bool IsDisabled { get; }
        bool IsUser { get; }
        void Disable();
        void Enable();
        void Select();
        void Unselect();
        void Hover();
        void Draw();
        void Discard();
        void Initialize();
        void SetAndUpdateView(ICardData data, SeatType seatType, UiGameDataAccess dataAccess);
    }
}