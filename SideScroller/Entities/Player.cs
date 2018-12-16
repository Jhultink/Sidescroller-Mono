using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SideScroller.Components;
using SideScroller.Entities;
using SideScroller.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller
{
    public class Player : Entity
    {
        public const int PickupRange = 10;

        public Vector2 Velocity;
        public Vector2 Position;
        public Inventory Inventory;
        public Rectangle Bounds;

        public Vector2 TilePosition => Position / Tile.Size;
        
        #region Components
        private InputComponent _input;
        private PhysicsComponent _physics;
        #endregion

        public Player(int x, int y, InputComponent input, PhysicsComponent physics)
        {
            _input = input;
            _physics = physics;

            Position = new Vector2(x * Tile.Size, y * Tile.Size);
        }

        public override void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("player");
        }

        public override void Update(GameTime gameTime)
        {
            _input.Update(this, gameTime);
            _physics.Update(this, gameTime);
        }   

        public void Draw(SpriteBatch batch, Camera camera)
        {
            batch.Draw(texture, Bounds, Color.White);
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (Bounds.TouchTopOf(newRectangle))
            {
                Bounds.Y = newRectangle.Y - Bounds.Height;
                Velocity.Y = 0f;
                //hasJumped = false;
            }

            if (Bounds.TouchLeftOf(newRectangle))
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
                Velocity.Y = 1f;
            }

            if (Position.X < 0)
                Position.X = 0;
            if (Position.X > xOffset * Tile.Size - Bounds.Width)
                Position.X = xOffset * Tile.Size - Bounds.Width;
            if (Position.Y < 0)
                Velocity.Y = 1f;
            if (Position.Y > yOffset * Tile.Size - Bounds.Height)
                Position.Y = yOffset * Tile.Size - Bounds.Height;
        }
    }
}
