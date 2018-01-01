using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SideScroller.World.Tiles;

namespace MooleyMania.World.Tiles
{
    class Air : Tile
    {
        public Air(int xCord, int yCord, TileOrientation orientation, Map map) : base(xCord, yCord, TileType.Air, orientation, map)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }
    }
}
