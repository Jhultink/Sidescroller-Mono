using Microsoft.Xna.Framework;
using MooleyMania;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.Helpers
{
    public static class VectorHelpers
    {
        public static Vector2 ToTile(this Vector2 pixelVector)
        {
            return new Vector2(pixelVector.X / Tile.Size, pixelVector.Y / Tile.Size);
        }
    }
}
