using snake_xyz.modules.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.input
{
    internal abstract class Input
    {
        public abstract void Subscribe(IArrowHandler listener);
        public abstract void Update();
    }
}
