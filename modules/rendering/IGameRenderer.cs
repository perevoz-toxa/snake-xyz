using snake_xyz.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.rendering
{
    internal interface IGameRenderer
    {
        int Width { get; }
        int Height { get; }

        void DrawFrame(GameState state);
        void Clear();
        void DrawTextScreen(string text);
    }
}
