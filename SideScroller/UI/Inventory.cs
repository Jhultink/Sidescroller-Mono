using Microsoft.Xna.Framework;
using MooleyMania;
using MooleyMania.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SideScroller.UI
{
    public class Inventory : UIElement
    {
        public const float padding = 100f;

        public const int horizSlotCount = 10;
        public const int vertSlotCount = 4;

        InventorySlot[,] slots = new InventorySlot[horizSlotCount, vertSlotCount];

        public Inventory(Camera camera) : base(new Vector2(padding), "inventoryBackground", (int) (camera.Bounds.Width - (2 * padding)), (int) (camera.Bounds.Height - (2 * padding)), false)
        {
            float slotPart = this.Width / (float)((horizSlotCount * 6) - 1);
            float slotSize = slotPart * 5;
            float slotMargin = slotPart;
            float vertPadding = (this.Height - (vertSlotCount * slotSize) - (vertSlotCount * (slotMargin - 1))) / 2;

            for (int x = 0; x < horizSlotCount; x++)
            {
                for (int y = 0; y < vertSlotCount; y++)
                {
                    float slotX = this.RelativePosition.X + (x * (slotSize + slotMargin));
                    float slotY = this.RelativePosition.Y + (y * (slotSize + slotMargin)) + vertPadding;
                    slots[x, y] = new InventorySlot(new Vector2(slotX, slotY), (int)slotSize, (int)slotSize);
                }
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            for(int x = 0; x < horizSlotCount; x++)
            {
                for (int y = 0; y < vertSlotCount; y++)
                {
                    slots[x, y].Load(content);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            // Render cells
            if (this.IsVisible)
            {
                for (int x = 0; x < horizSlotCount; x++)
                {
                    for (int y = 0; y < vertSlotCount; y++)
                    {
                        slots[x, y].Draw(spriteBatch, camera);
                    }
                }
            }
        }

        public void AddItems(TileType type, int count)
        {
            // Try to add to existing slot
            for (int x = 0; x < horizSlotCount; x++)
            {
                for (int y = 0; y < vertSlotCount; y++)
                {
                    if(slots[x, y].Type == type)
                    {
                        slots[x, y].AddItems(type, count);
                        return;
                    }
                }
            }

            // Add to an empty slot
            for (int x = 0; x < horizSlotCount; x++)
            {
                for (int y = 0; y < vertSlotCount; y++)
                {
                    if (slots[x, y].Type == null)
                    {
                        slots[x, y].AddItems(type, count);
                        return;
                    }
                }
            }
        }
    }
}
