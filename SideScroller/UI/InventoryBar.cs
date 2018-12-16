using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using SideScroller.UI;

namespace SideScroller.UI
{
    public class InventoryBar : UIElement
    {
        private static int slotCount = 10;

        private InventorySlot[] slots = new InventorySlot[slotCount];

        public InventoryBar(Camera camera) : base(new Vector2(camera.Bounds.Right - 50, camera.Bounds.Top + 50), @"InventoryBackground", 40, camera.Bounds.Height - 100)
        {
            DrawSlots();
        }

        private void DrawSlots()
        {
            float slotPart = this.Height / (float) ((slotCount * 6) - 1);
            float slotSize = slotPart * 5;
            float slotMargin = slotPart;
            int horizMargin = (int)((this.Width - slotSize) / 2f);

            for (int i = 0; i < slotCount; i++)
            {
                Vector2 pos;

                if (i < slotCount - 1)
                    pos = new Vector2(horizMargin, i * (slotSize + slotMargin));
                else
                    pos = new Vector2(horizMargin, this.Height - slotSize);

                slots[i] = new InventorySlot(this.RelativePosition + pos, (int) slotSize, (int) slotSize);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
            foreach(InventorySlot slot in slots)
            {
                slot.Draw(spriteBatch, camera);
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            foreach (InventorySlot slot in slots)
            {
                slot.Load(content);
            }
        }

        public override void UpdateRelativePosition(Vector2 relPos)
        {
            base.UpdateRelativePosition(relPos);

            float slotPart = this.Height / (float)((slotCount * 6) - 1);
            float slotSize = slotPart * 5;
            float slotMargin = slotPart;
            int horizMargin = (int)((this.Width - slotSize) / 2f);

            for (int i = 0; i < slotCount; i++)
            {
                Vector2 pos;

                if (i < slotCount - 1)
                    pos = new Vector2(horizMargin, i * (slotSize + slotMargin));
                else
                    pos = new Vector2(horizMargin, this.Height - slotSize);

                slots[i].Width = (int)slotSize;
                slots[i].Height = (int)slotSize;
                slots[i].RelativePosition = this.RelativePosition + pos;

            }
        }        
    }
}
