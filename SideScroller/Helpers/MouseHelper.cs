using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooleyMania.Helpers
{
    public static class MouseHelper
    {
        /// <summary>
        /// Returns an absolute pixel value of mouse relative to the camera
        /// </summary>
        /// <param name="state"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector2 ToAbsolute(this MouseState state, Camera camera)
        {
            //return new Vector2(state.X + camera.Center.X - (camera.Bounds.Width / 2), 
            //    state.Y + camera.Center.Y - (camera.Bounds.Height / 2));

            return new Vector2(state.X, state.Y) + camera.Position;
        }
    }
}
