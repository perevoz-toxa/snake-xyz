using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.controller
{
    public interface IArrowHandler
    {
        void OnArrowUp();
        void OnArrowDown();
        void OnArrowLeft();
        void OnArrowRight();
    }
}
