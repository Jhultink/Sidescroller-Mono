using Microsoft.Xna.Framework;
using SideScroller;
using SideScroller.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller.UI
{
    public class InventorySlot : UIElement
    {

        private ContentManager content;
        private Texture2D itemTexture;
        private SpriteFont countFont;

        public TileType? Type { get; set; }

        public int Count { get; set; } = 0;

        public InventorySlot(Vector2 relativePosition, int width, int height) : base(relativePosition, "inventorySlot", width, height)
        {
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            this.content = content;
            this.countFont = content.Load<SpriteFont>("MainFont");
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
            if(this.Type != null && this.Count != 0)
            {
                float x = this.AbsolutePosition.X + (this.Width / 4f); // 25% margin
                float y = this.AbsolutePosition.Y + (this.Height / 4f); // 25% margin
                spriteBatch.Draw(this.itemTexture, new Rectangle((int)x, (int)y, (int)(this.Width * .5f), (int)(this.Height * .5f)), Color.White); // 50% margin
                spriteBatch.DrawString(countFont, this.Count.ToString(), this.AbsolutePosition + new Vector2(this.Width * .6f, this.Height * .6f), Color.Black);
            }
        }

        public void AddItems(TileType type, int count)
        {
            if (this.Type == null)
            {
                this.Type = type;
                this.itemTexture = this.content.Load<Texture2D>(this.Type.ToString());
            }
            else if (this.Type != type)
                throw new ArgumentException();

            this.Count += count;
        }
    }
}
