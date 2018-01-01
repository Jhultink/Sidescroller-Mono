using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MooleyMania.World.Tiles;
using SideScroller.World.Tiles;
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
        public TileOrientation Orientation;
        protected ContentManager content;
        protected Map map;
        protected Texture2D texture;
        protected int xTile;
        protected int yTile;

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

        protected Tile(int xCord, int yCord, TileType type, TileOrientation orientation, Map map)
        {
            this.xTile = xCord;
            this.yTile = yCord;
            this.map = map;
            this.Type = type;
            this.Orientation = orientation;
            this.Rectangle = new Rectangle(xCord * Tile.Size, (yCord * Tile.Size), Tile.Size, Tile.Size);
        }
    }
}
