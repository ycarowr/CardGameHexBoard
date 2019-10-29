using Tools.Patterns.Observer;

namespace HexCardGame.Model.Game
{
    /// <summary> Small Part of a Turn. </summary>
    public abstract class BaseGameMechanics
    {
        protected BaseGameMechanics(IGame game) => Game = game;

        protected IGame Game { get; }

        protected IDispatcher Dispatcher => Game.Dispatcher;
    }
}