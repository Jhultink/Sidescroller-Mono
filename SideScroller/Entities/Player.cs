using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SideScroller.Entities;
using SideScroller.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooleyMania
{
    public class Player : Entity
    {
        private const float VERTICAL_VELOCITY = 3f;

        private const float HORIZONTAL_VELOCITY = 6f;

        public const int PickupRange = 10;

        public Vector2 velocity;

        public Vector2 Position;

        public Inventory Inventory;

        public Vector2 TilePosition { get { return Position / Tile.Size; } }

        public Rectangle Bounds;

        private bool hasJumped = false;

        public Player(int x, int y)
        {
            Position = new Vector2(x * Tile.Size, y * Tile.Size);
        }

        public override void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("player");
        }

        public override void Update(GameTime gameTime)
        {
            Position += velocity;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Tile.Size * 2, Tile.Size * 3);

            Input(gameTime);

            if (velocity.Y < 10)
                velocity.Y += 0.4f;
        }
        
        public void Input(GameTime gameTime)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //     velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            //else if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            //else
            //    velocity.X = 0f;


            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = HORIZONTAL_VELOCITY;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = -HORIZONTAL_VELOCITY;
            else
                velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hasJumped)
            {
                Position.Y -= 3f;
                velocity.Y = -VERTICAL_VELOCITY;
                hasJumped = true;
            }
        }
        
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {             
            if(Bounds.TouchTopOf(newRectangle))
            {
                Bounds.Y = newRectangle.Y - Bounds.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if(Bounds.TouchLeftOf(newRectangle))
            {
                Position.X = newRectangle.X - Bounds.Width - 1;
                //position.X = newRectangle.X + newRectangle.Width + 1;
            }
            if (Bounds.TouchRightOf(newRectangle))
            {
                Position.X = newRectangle.X + newRectangle.Width + 1;
                //position.X = newRectangle.X - rectangle.Width - 2;

            }

            if (Bounds.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }


            if (Position.X < 0)
                Position.X = 0;
            if (Position.X > xOffset * Tile.Size - Bounds.Width)
                Position.X = xOffset * Tile.Size - Bounds.Width;
            if (Position.Y < 0)
                velocity.Y = 1f;
            if (Position.Y > yOffset * Tile.Size - Bounds.Height)
                Position.Y = yOffset * Tile.Size - Bounds.Height;
        }


        public void Draw(SpriteBatch batch, Camera camera)
        {
            batch.Draw(texture, Bounds, Color.White);
        }
    }
}
