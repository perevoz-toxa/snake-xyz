using snake_xyz.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.rendering
{
    internal class SnakeConsoleRenderer : IGameRenderer
    {
        private readonly ConsoleRenderer _buffer0;
        private readonly ConsoleRenderer _buffer1;

        private ConsoleRenderer _prev;
        private ConsoleRenderer _curr;

        public SnakeConsoleRenderer(ConsoleColor[] palette)
        {
            _buffer0 = new ConsoleRenderer(palette);
            _buffer1 = new ConsoleRenderer(palette);

            _buffer0.bgColor = ConsoleColor.Black;
            _buffer1.bgColor = ConsoleColor.Black;

            _prev = _buffer0;
            _curr = _buffer1;
        }

        public void Clear()
        {
            _curr.Clear();
        }

        public void DrawFrame(GameState state)
        {
            int w = state.fieldWidth;
            int h = state.fieldHeight;

            for (int x = 0; x < w + 2; x++)
            {
                _curr.SetPixel(x, 0, '#', 1);
                _curr.SetPixel(x, h + 1, '#', 1);
            }

            for (int y = 0; y < h + 2; y++)
            {
                _curr.SetPixel(0, y, '#', 1);
                _curr.SetPixel(w + 1, y, '#', 1);
            }

            // 2. Рисуем змейку внутри рамки, со смещением на +1,+1
            foreach (var (x, y) in state.GetBody())
            {
                _curr.SetPixel(x + 1, y + 1, '■', 1);
            }

            if (!_curr.Equals(_prev))
            {
                _curr.Render();
            }

            var tmp = _prev;
            _prev = _curr;
            _curr = tmp;
        }
    }
}