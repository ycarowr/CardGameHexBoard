using System.Collections.Generic;
using HexCardGame.Model;
using HexCardGame.Model.GameBoard;
using HexCardGame.Model.GamePool;

namespace HexCardGame
{
    /// <summary> Broadcast of the players right before the game start. </summary>
    [Event]
    public interface IPreGameStart
    {
        void OnPreGameStart(List<IPlayer> players);
    }

    /// <summary> Broadcast of the starter player to the Listeners. </summary>
    [Event]
    public interface IStartGame
    {
        void OnStartGame(IPlayer starter);
    }

    /// <summary> Broadcast of the winner after a game is finished to the Listeners. </summary>
    [Event]
    public interface IFinishGame
    {
        void OnFinishGame(IPlayer winner);
    }

    /// <summary> Broadcast of a player when it starts the turn to the Listeners. </summary>
    [Event]
    public interface IStartPlayerTurn
    {
        void OnStartPlayerTurn(IPlayer player);
    }

    /// <summary> Broadcast of a player when it finishes the turn to the Listeners. </summary>
    [Event]
    public interface IFinishPlayerTurn
    {
        void OnFinishPlayerTurn(IPlayer player);
    }

    /// <summary> Broadcast of restart game. </summary>
    [Event]
    public interface IRestartGame
    {
        void OnRestart();
    }

    [Event]
    public interface ICreateBoard
    {
        void OnCreateBoard(Board board);
    }
    
    [Event]
    public interface ICreatePool
    {
        void OnCreatePool(Pool pool);
    }
    
    [Event]
    public interface ICreateLibrary
    {
        void OnCreateLibrary(Library library);
    }
    
}