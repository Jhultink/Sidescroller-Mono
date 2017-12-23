using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MooleyMania.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooleyMania
{
    public abstract class Tile : ITile
    {

        public const int Size = 16;
        public TileType Type;

        protected Texture2D texture;

        public Rectangle Rectangle { get; protected set; }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Load(ContentManager content);

        protected Tile(int xCord, int yCord, TileType type)
        {
            this.Type = type;
            this.Rectangle = new Rectangle(xCord * Tile.Size, (yCord * Tile.Size), Tile.Size, Tile.Size);

        }
    }
}
