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
    public class Dirt : Tile
    {
        public Dirt(int xCord, int yCord, TileOrientation orientation, Map map) : base(xCord, yCord, TileType.Dirt, orientation, map)
        {
        }

        public override void Load(ContentManager content)
        {
            this.content = content;

            bool left = false;
            bool right = false;
            bool top = false;
            bool bottom = false;

            if(xTile >= 1)
                left = map[this.xTile - 1, this.yTile].Type != TileType.Air;
            if (xTile + 1 < map.MaxWidth)
                right = map[this.xTile + 1, this.yTile].Type != TileType.Air;
            if (yTile >= 1)
                top = map[this.xTile, this.yTile - 1].Type != TileType.Air;
            if (yTile + 1 < map.MaxHeight)
                bottom = map[this.xTile, this.yTile + 1].Type != TileType.Air;

            int edgeCount = 0;
            if (left) edgeCount++;
            if (right) edgeCount++;
            if (top) edgeCount++;
            if (bottom) edgeCount++;

            if(edgeCount > 2)
            {
                texture = content.Load<Texture2D>(@"Dirt\Backdrop" + (new Random().Next(4) + 1));
            }
            else if (top && right)
            {
                texture = content.Load<Texture2D>(@"Dirt\TopRight");
            }
            else if (top && left)
            {
                texture = content.Load<Texture2D>(@"Dirt\TopLeft");
            }
            else if (bottom && right)
            {
                texture = content.Load<Texture2D>(@"Dirt\BottomRight");
            }
            else if (bottom && left)
            {
                texture = content.Load<Texture2D>(@"Dirt\BottomLeft");
            }
            else
            {
                texture = content.Load<Texture2D>(@"Dirt\Backdrop" + (new Random().Next(4) + 1));
            }
        }
    }
}
