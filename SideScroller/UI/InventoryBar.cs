using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace MooleyMania.UI
{
    public class InventoryBar : UIElement
    {
        private static int slotCount = 30;
        private static int slotSize = 40;

        private InventoryBarSlot[] slots;

        public InventoryBar(Camera camera) : base(new Vector2(camera.Bounds.Right - 50, camera.Bounds.Top + 50), @"InventoryBackground", 50, camera.Bounds.Height - 100)
        {
            float heightAndMargin = (this.Height - slotSize) / (slotCount - 1);

            if(heightAndMargin < slotSize)
            {
                slotCount = this.Height / slotSize;
                heightAndMargin = (this.Height - slotSize) / (slotCount - 1);
            }

            slots = new InventoryBarSlot[slotCount];

            for (int i = 0; i < slotCount; i++)
            {
                Vector2 pos;

                if (i < slotCount - 1)
                    pos = new Vector2(5, i * heightAndMargin);
                else
                    pos = new Vector2(5, this.Height - slotSize);

                slots[i] = new InventoryBarSlot(this.RelativePosition + pos, slotSize, slotSize);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);
            foreach(InventoryBarSlot slot in slots)
            {
                slot.Draw(spriteBatch, camera);
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            foreach (InventoryBarSlot slot in slots)
            {
                slot.Load(content);
            }
        }

        public override void UpdateRelativePosition(Vector2 relPos)
        {
            base.UpdateRelativePosition(relPos);

            
        }

        class InventoryBarSlot : UIElement
        {
            public InventoryBarSlot(Vector2 relativePosition, int width, int height) : base(relativePosition, "inventorySlot", width, height)
            {

            }
        }
    }
}
