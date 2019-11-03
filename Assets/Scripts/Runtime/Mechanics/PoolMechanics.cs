using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.Runtime.Game
{
    public class PoolMechanics: BaseGameMechanics
    {
        public PoolMechanics(IGame game) : base(game)
        {
        }

        public void UncoverCard(PoolPositionIndex positionIndex)
        {
            if (!Game.IsGameStarted)
                return;
            
            var poolCard = Game.Pool.GetCardAt(positionIndex);
            if (poolCard.IsCovered)
                return;

            Game.Pool.UncoverAt(positionIndex);
        }
        
        public void CoverCard(PoolPositionIndex positionIndex)
        {
            if (!Game.IsGameStarted)
                return;
            
            var poolCard = Game.Pool.GetCardAt(positionIndex);
            if (!poolCard.IsCovered)
                return;

            Game.Pool.CoverAt(positionIndex);
        }
    }
}