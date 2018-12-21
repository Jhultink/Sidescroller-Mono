using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SideScroller.ScrollerWorld.Tiles;

namespace SideScroller.ScrollerWorld.Tiles
{
    class Air : Tile
    {
        public Air(int xCord, int yCord, Map map, World world) : base(xCord, yCord, TileType.Air, map, world)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(this.yTile > map.hillsStart)
            {
                base.Draw(spriteBatch);
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            body.CollisionCategories = Category.Cat2;
            body.CollidesWith = Category.None;

            if (this.yTile > map.hillsStart)
            {
                this.texture = content.Load<Texture2D>(@"Backdrops/Backdrop" + (new Random().Next(4) + 1));
            }
        }
    }
}
