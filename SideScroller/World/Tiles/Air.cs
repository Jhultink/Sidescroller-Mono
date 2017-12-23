using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MooleyMania.World.Tiles
{
    class Air : Tile
    {
        public Air(int xCord, int yCord) : base(xCord, yCord, TileType.Air)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Load(ContentManager content)
        {
        }
    }
}
