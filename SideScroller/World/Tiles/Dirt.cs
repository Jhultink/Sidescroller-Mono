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
        public Dirt(int xCord, int yCord, Map map) : base(xCord, yCord, TileType.Dirt, map)
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
                left = map[this.xTile - 1, this.yTile].Type == this.Type;
            if (xTile + 1 < map.MaxWidth)
                right = map[this.xTile + 1, this.yTile].Type == this.Type;
            if (yTile >= 1)
                top = map[this.xTile, this.yTile - 1].Type == this.Type;
            if (yTile + 1 < map.MaxHeight)
                bottom = map[this.xTile, this.yTile + 1].Type == this.Type;

            int edgeCount = 0;
            if (left) edgeCount++;
            if (right) edgeCount++;
            if (top) edgeCount++;
            if (bottom) edgeCount++;

            Random rand = new Random();

            if (top && right && bottom && left)
            {
                this.texture = content.Load<Texture2D>(this.Type + "/Fill" + (rand.Next(2) + 1));
            }
            else if (!top && right && bottom && left)
            {
                this.texture = content.Load<Texture2D>(this.Type + "/Top" + (rand.Next(3) + 1));
            }            
            else if (!top && !left && bottom && right)
            {
                this.texture = content.Load<Texture2D>(this.Type + "/TopLeft");
            }
            else if (!top && left && bottom && !right)
            {
                this.texture = content.Load<Texture2D>(this.Type + "/TopRight");
            }
            else if (top && right && !left)
            {
                this.texture = content.Load<Texture2D>(this.Type + "/Left" + (rand.Next(2) + 1));
            }
            else if (top && !right && left)
            {
                this.texture = content.Load<Texture2D>(this.Type + "/Right" + (rand.Next(2) + 1));
            }
            else
            {
                this.texture = content.Load<Texture2D>(this.Type + "/Fill" + (rand.Next(2) + 1));
            }

        }
    }
}
