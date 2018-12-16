using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SideScroller.World.Tiles;

namespace SideScroller.World.Tiles
{
    class Air : Tile
    {
        public Air(int xCord, int yCord, Map map) : base(xCord, yCord, TileType.Air, map)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(this.yTile > map.hillsStart)
            {
                base.Draw(spriteBatch);
            }
        }

        public override void Load(ContentManager content)
        {
            if (this.yTile > map.hillsStart)
            {
                this.texture = content.Load<Texture2D>(@"Backdrops/Backdrop" + (new Random().Next(4) + 1));
            }
        }
    }
}
