using FarseerPhysics.Dynamics;
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
        private const float VERTICAL_VELOCITY = 12f;
        private const float HORIZONTAL_VELOCITY = 3f;

        public void Update(Body body, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                body.ApplyLinearImpulse(new Vector2(HORIZONTAL_VELOCITY, 0));
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                body.ApplyLinearImpulse(new Vector2(-HORIZONTAL_VELOCITY, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                body.ApplyLinearImpulse(new Vector2(0, -VERTICAL_VELOCITY));
            }
        }
    }
}
