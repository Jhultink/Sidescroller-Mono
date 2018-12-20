using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller.ScrollerWorld.Tiles
{
    interface ITile
    {
        void Load(ContentManager content);

        void Draw(SpriteBatch spriteBatch);

        void Clicked(GameTime gameTime);
    }

}
