using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SideScroller.Components
{
    public class PhysicsComponent : IComponent
    {
        private Rectangle topRect;
        private Rectangle rightRect;
        private Rectangle bottomRect;
        private Rectangle leftRect;

        public void Update(Player player, GameTime gameTime)
        {
            player.Position += player.Velocity;
            player.Bounds = new Rectangle((int)player.Position.X, (int)player.Position.Y, Tile.Size * 2, Tile.Size * 3);

            topRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, Tile.Size * 2, Tile.Size);

            bottomRect = topRect = new Rectangle((int)player.Position.X, (int)player.Position.Y + Tile.Size * 2, Tile.Size * 2, Tile.Size);

            if (player.Velocity.Y < 10)
                player.Velocity.Y += 0.4f;
        }
    }
}
