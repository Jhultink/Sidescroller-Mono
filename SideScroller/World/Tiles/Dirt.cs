using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MooleyMania.World.Tiles
{
    public class Dirt : Tile
    {
        public Dirt(int xCord, int yCord) : base(xCord, yCord, TileType.Dirt)
        {
        }

        public override void Load(ContentManager content)
        {
            this.content = content;

            if (new Random().Next(2) == 1)
                texture = content.Load<Texture2D>(@"Dirt\Fill1");
            else
                texture = content.Load<Texture2D>(@"Dirt\Fill2");

        }
    }
}
