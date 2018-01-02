using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MooleyMania;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.World
{
    public class Background
    {
        private Texture2D sky;
        private Texture2D clouds;
        private Texture2D sea;


        public void Load(ContentManager content)
        {
            this.sky = content.Load<Texture2D>("sky");
            this.clouds = content.Load<Texture2D>("clouds");
            this.sea = content.Load<Texture2D>("sea");

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Rectangle topTwoThirds = new Rectangle((int)camera.Position.X, (int)camera.Position.Y, camera.Bounds.Width, (int) (camera.Bounds.Height * ( 2f / 3f)));
            spriteBatch.Draw(sky, topTwoThirds, Color.White);

            Rectangle middleThird = new Rectangle((int)camera.Position.X, (int)(camera.Position.Y + (camera.Bounds.Height / 3f)), camera.Bounds.Width, (int)(camera.Bounds.Height / 3f));
            spriteBatch.Draw(clouds, middleThird, Color.White);

            Rectangle bottomThird = new Rectangle((int)camera.Position.X, (int)(camera.Position.Y + (camera.Bounds.Height * (2f / 3f))), camera.Bounds.Width, (int)(camera.Bounds.Height / 3f));
            spriteBatch.Draw(sea, bottomThird, Color.White);

        }
    }
}
