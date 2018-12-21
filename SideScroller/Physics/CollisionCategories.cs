using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller.Physics
{
    public enum CollisionCategories : short
    {
        Blocks = 1,
        Air = 2,
        All =  short.MaxValue
    }
}
