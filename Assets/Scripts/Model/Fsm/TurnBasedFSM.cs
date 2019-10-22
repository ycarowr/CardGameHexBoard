using System.Collections.Generic;
using HexCardGame.Model.Game;
using Tools.Patterns.StateMachine;

namespace HexCardGame
{
    public class TurnBasedFsm : BaseStateMachine
    {
        Dictionary<PlayerId, TurnState> _register = new Dictionary<PlayerId, TurnState>(); 
        public IGameController Controller { get; }

        public TurnBasedFsm(IGameController controller, IGame gameData, 
            GameParameters gameParameters, EventsDispatcher gameEvents) : base(controller)
        {
            Controller = controller;
            
            //create states
            var user = new UserPlayer(this, gameData, gameParameters, gameEvents);
            var enemy = new EnemyPlayer(this, gameData, gameParameters, gameEvents);
            var start = new StartBattle(this, gameData, gameParameters, gameEvents);
            var end = new EndBattle(this, gameData, gameParameters, gameEvents);

            //register all states
            RegisterState(user);
            RegisterState(enemy);
            RegisterState(start);
            RegisterState(end);
            
            Initialize();
        }


        /// <summary>  Call this method to Push Start Battle State and begin the match. </summary>
        public void StartBattle()
        {
            if (!IsInitialized)
                return;

            PopState();
            PushState<StartBattle>();
        }

        /// <summary>  Call this method to Push End Battle State and Finish the match. </summary>
        public void EndBattle()
        {
            if (!IsInitialized)
                return;

            PopState();
            PushState<EndBattle>();
        }
        
        public void UserTurn()
        {
            if (!IsInitialized)
                return;

            PopState();
            PushState<UserPlayer>();
        }
        
        public void EnemyPlayer()
        {
            if (!IsInitialized)
                return;

            PopState();
            PushState<EnemyPlayer>();
        }

        public void RegisterPlayer(PlayerId id, TurnState state) => _register.Add(id, state);
        public TurnState GetPlayerState(PlayerId id) => _register[id];
        public override void Clear()
        {
            base.Clear();
            _register.Clear();
        }
    }
}