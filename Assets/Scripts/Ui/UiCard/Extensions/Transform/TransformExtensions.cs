using System;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        ///     Calculus of the location of this object. Whether it is located at the top or bottom. -1 and 1 respectively.
        /// </summary>
        /// <returns></returns>
        public static int CloserEdge(this Transform transform, Camera camera, int width, int height)
        {
            //edge points according to the screen/camera
            var worldPointTop = camera.ScreenToWorldPoint(new Vector3(width / 2, height));
            var worldPointBot = camera.ScreenToWorldPoint(new Vector3(width / 2, 0));

            //distance from the pivot to the screen edge
            var deltaTop = Vector2.Distance(worldPointTop, transform.position);
            var deltaBottom = Vector2.Distance(worldPointBot, transform.position);

            return deltaBottom <= deltaTop ? 1 : -1;
        }
    }
}