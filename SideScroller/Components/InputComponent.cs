using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SideScroller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.Components
{
    public class InputComponent
    {
        private const float VERTICAL_VELOCITY = 2f;
        private const float HORIZONTAL_VELOCITY = 3f;
        private bool hasJumped = false;

        public void Update(Player player, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                player.Velocity.X = HORIZONTAL_VELOCITY;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                player.Velocity.X = -HORIZONTAL_VELOCITY;
            else
                player.Velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hasJumped)
            {
                //player.Position.Y -= 3f;
                player.Velocity.Y = -VERTICAL_VELOCITY;
                hasJumped = true;
            }
        }
    }
}
