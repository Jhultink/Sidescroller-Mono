using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MooleyMania.World.Tiles
{
    interface ITile
    {
        void Load(ContentManager content);
        void Draw(SpriteBatch spriteBatch);
    }

}
