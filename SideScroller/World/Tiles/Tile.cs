﻿using Microsoft.Xna.Framework;
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
        public const int Size = 12;
        public TileType Type;
        protected ContentManager content;

        protected Texture2D texture;

        public Rectangle Rectangle { get; protected set; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public virtual void Load(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>(Type.ToString());
        }

        public virtual void Clicked(GameTime gameTime)
        {
            this.Type = TileType.Air;
            this.texture = this.content.Load<Texture2D>(TileType.Air.ToString());
        }

        protected Tile(int xCord, int yCord, TileType type)
        {
            this.Type = type;
            this.Rectangle = new Rectangle(xCord * Tile.Size, (yCord * Tile.Size), Tile.Size, Tile.Size);
        }
    }
}
