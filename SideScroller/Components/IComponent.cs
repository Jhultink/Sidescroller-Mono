using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.Components
{
    public interface IComponent
    {
        void Update(Player player, GameTime gameTime);
    }
}
