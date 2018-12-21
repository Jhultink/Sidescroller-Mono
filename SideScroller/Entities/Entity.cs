using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.Entities
{
    public abstract class Entity
    {
        protected Texture2D texture;
        protected Body body;
        protected World world;

        public abstract void Load(ContentManager Content);

        public abstract void Update(GameTime gameTime);

    }
}
