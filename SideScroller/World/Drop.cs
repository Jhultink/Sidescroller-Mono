using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SideScroller;
using SideScroller.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.World
{
    public class Drop
    {
        private const int Size = 8;

        public static ContentManager Content { get; set; }

        public Vector2 Position { get; set; }

        public TileType Type { get; private set; }

        private Vector2 velocity;
        private Rectangle bounds;
        private Texture2D texture;

        public Drop(int tileX, int tileY, TileType type)
        {
            float centeringOffset = (Tile.Size - Drop.Size) / 2.0f;

            Position = new Vector2((tileX * Tile.Size) + centeringOffset, (tileY * Tile.Size) + centeringOffset);
            velocity = new Vector2();
            this.Type = type;
            texture = Content.Load<Texture2D>(type.ToString());
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, bounds, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Position += velocity;

            bounds = new Rectangle((int)Position.X, (int)Position.Y, Size, Size);
        }

        public void DetectCollisions(Map map)
        {
            int xCollisionUpdateDistance = 5;

            bool collision = false;
            for (int x = (int)this.Position.ToTile().X - xCollisionUpdateDistance; x < (int)this.Position.ToTile().X + xCollisionUpdateDistance; x++)
            {
                for (int y = (int)this.Position.ToTile().Y - xCollisionUpdateDistance; y < (int)this.Position.ToTile().Y + xCollisionUpdateDistance; y++)
                {
                    if (x >= 0 && x < map.MaxWidth && y >= 0 && y < map.MaxHeight)
                    {
                        if (map.Tiles[x, y].Type != TileType.Air)
                        {
                            if (this.bounds.TouchTopOf(map.Tiles[x, y].Rectangle))
                                collision = true;
                        }
                    }
                }
            }

            if (collision)
                this.velocity.Y = 0;
            else
                this.velocity.Y = 1;
        }

        public void MoveToPlayer(Player player)
         {
            int dropSpeed = 2;
            this.velocity = Vector2.Normalize(player.Position - this.Position) * dropSpeed;
        }
    }
}
