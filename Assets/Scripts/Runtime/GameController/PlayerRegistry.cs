using System;
using UnityEngine;

namespace HexCardGame
{
    [RequireComponent(typeof(GameController))]
    public class PlayerRegistry : MonoBehaviour
    {
        private EventsDispatcher _dispatcher;
        private GameController _gameController;
        private GameParameters _gameParameters;
        [SerializeField] private PlayerEntry[] registry;

        private void Awake()
        {
            _gameController = GetComponent<GameController>();
            _dispatcher = EventsDispatcher.Load();
            _gameParameters = GameParameters.Load();
        }

        public IPlayer GetPlayer(SeatType seatType)
        {
            foreach (var entry in registry)
                if (entry.seat == seatType)
                    return entry.player;

            return null;
        }

        public SeatType GetSeat(IPlayer player)
        {
            foreach (var entry in registry)
                if (entry.player == player)
                    return entry.seat;

            throw new ArgumentException($"Player {player} is not registered");
        }
        
        private bool IsReady()
        {
            foreach (var entry in registry)
                if (!entry.IsValid)
                    return false;
            return true;
        }

        private void HandleSpawnPlayer()
        {
            var localPlayerSeat = _gameParameters.Profiles.localPlayer.seat;
            var remotePlayerSeat = _gameParameters.Profiles.remotePlayer.seat;

            var localPlayerNetworkId = 0;
            var remotePlayerNetworkId = 1;

            var localPlayer = new Player(localPlayerNetworkId, localPlayerSeat, _gameParameters, _dispatcher);
            var remotePlayer = new Player(remotePlayerNetworkId, remotePlayerSeat, _gameParameters, _dispatcher);

            OnConnectPlayer(localPlayer);
            OnConnectPlayer(remotePlayer);
            
            if(IsReady())
                _gameController.StartBattle(localPlayer, remotePlayer);
        }

        void OnConnectPlayer(IPlayer player)
        {
            var playerSeat = player.Seat;
            var entry = GetEntry(playerSeat);
            entry.player = player;
        }

        private ref PlayerEntry GetEntry(SeatType seatType)
        {
            for (var index = 0; index < registry.Length; index++)
            {
                var entry = registry[index];
                if (entry.seat == seatType)
                    return ref registry[index];
            }
            
            throw new ArgumentException($"Entry {seatType} is not registered");
        }

        [Serializable]
        private struct PlayerEntry
        {
            public IPlayer player;
            public SeatType seat;
            public bool IsValid => player != null;
        }
    }
}