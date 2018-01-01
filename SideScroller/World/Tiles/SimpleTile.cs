using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SideScroller.World.Tiles;

namespace MooleyMania.World.Tiles
{
    class SimpleTile : Tile
    {
        public SimpleTile(int xCord, int yCord, TileType type, TileOrientation orientation, Map map) : base(xCord, yCord, type, orientation, map)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public override void Load(ContentManager content)
        {
            this.texture = content.Load<Texture2D>(this.Type.ToString());
        }
    }
}
