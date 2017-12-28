using Microsoft.Xna.Framework;
using MooleyMania.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.UI
{
    public class InventorySlot : UIElement
    {
        public InventorySlot(Vector2 relativePosition, int width, int height) : base(relativePosition, "inventorySlot", width, height)
        {

        }
    }
}
