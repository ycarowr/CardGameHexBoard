using System.Collections.Generic;
using Extensions;
using HexCardGame.Runtime.GamePool;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiPoolPositioning
    {
        public const int LayerZ = 3;
        public const int MaxRowsX = 4;
        public const int MaxColumnsY = 4;
        readonly Dictionary<PositionId, Vector3> _positions = new Dictionary<PositionId, Vector3>();

        public UiPoolPositioning(UiPool pool, UiPoolParameters parameters)
        {
            Pool = pool;
            Parameters = parameters;
            Size = parameters.UiCardSize.Value;
            UpdatePositions();
        }

        public Vector3 Size { get; }
        public float Width => Size.x;
        public float Height => Size.y;
        public float HalfWidth => Width / 2;
        public float HalfHeight => Height / 2;
        public float MinX => GetMinX() - HalfWidth;
        public float MinY => GetMinY() - HalfHeight;
        public float MaxX => GetMaxX() + HalfWidth;
        public float MaxY => GetMaxY() + HalfHeight;


        UiPool Pool { get; }
        UiPoolParameters Parameters { get; }
        public void Update() => UpdatePositions();

        public Vector3 GetPositionFor(PositionId positionId) => _positions[positionId];

        void UpdatePositions()
        {
            var zero = Pool.transform.position;
            var cardsPerRow = MaxRowsX;
            var positionCount = 0;
            var totalY = HalfHeight + MaxColumnsY * Height + MaxColumnsY * Parameters.SpacingY;
            var minY = zero.y - totalY / 2;
            for (var i = 0; i < MaxColumnsY; i++)
            {
                var totalRow = cardsPerRow * Parameters.SpacingX + cardsPerRow * Width;
                var minRowX = zero.x - totalRow / 2;
                var pX = minRowX + HalfWidth;
                var pY = minY + HalfHeight + i * (Parameters.SpacingY + Height);
                var maxLoop = positionCount + cardsPerRow;
                for (var p = positionCount; p < maxLoop; p++)
                {
                    SetPosition((PositionId) p, new Vector3(pX, pY));
                    pX += Parameters.SpacingX + Width;
                }

                positionCount = maxLoop;
                cardsPerRow--;
            }
        }

        void SetPosition(PositionId i, Vector3 position)
        {
            if (!_positions.ContainsKey(i))
                _positions.Add(i, position);
            else
                _positions[i] = position.WithZ(LayerZ);
        }


        float GetMaxX()
        {
            var greaterX = -99999f;
            foreach (var i in _positions.Keys)
            {
                var value = _positions[i].x;
                if (value > greaterX)
                    greaterX = value;
            }

            return greaterX;
        }

        float GetMaxY()
        {
            var greaterY = -99999f;
            foreach (var i in _positions.Keys)
            {
                var value = _positions[i].y;
                if (value > greaterY)
                    greaterY = value;
            }

            return greaterY;
        }

        float GetMinX()
        {
            var smaller = 99999f;
            foreach (var i in _positions.Keys)
            {
                var value = _positions[i].x;
                if (value < smaller)
                    smaller = value;
            }

            return smaller;
        }

        float GetMinY()
        {
            var smaller = 99999f;
            foreach (var i in _positions.Keys)
            {
                var value = _positions[i].y;
                if (value < smaller)
                    smaller = value;
            }

            return smaller;
        }
    }
}