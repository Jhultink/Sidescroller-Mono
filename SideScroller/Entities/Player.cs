using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooleyMania
{
    public class Player
    {
        private Texture2D texture;

        public Vector2 velocity;

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        public Vector2 TilePosition { get { return position / Tile.Size; } }

        private Rectangle rectangle;

        private bool hasJumped = false;

        public Player(int x, int y)
        {
            position = new Vector2(x * Tile.Size, y * Tile.Size);
        }


        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("player");
        }


        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, Tile.Size * 2, Tile.Size * 3);

            Input(gameTime);

            if (velocity.Y < 10)
                velocity.Y += 0.4f;

        }


        public void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else velocity.X = 0f;

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && !hasJumped)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
        }


        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        { 
            
            if(rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if(rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 1;
                //position.X = newRectangle.X + newRectangle.Width + 1;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 1;
                //position.X = newRectangle.X - rectangle.Width - 2;

            }

            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }


            if (position.X < 0)
                position.X = 0;
            if (position.X > xOffset * Tile.Size - rectangle.Width)
                position.X = xOffset * Tile.Size - rectangle.Width;
            if (position.Y < 0)
                velocity.Y = 1f;
            if (position.Y > yOffset * Tile.Size - rectangle.Height)
                position.Y = yOffset * Tile.Size - rectangle.Height;
        }


        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, rectangle, Color.White);
        }
    }
}
