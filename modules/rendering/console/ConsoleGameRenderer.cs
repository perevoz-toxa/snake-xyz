using snake_xyz.state;
using System;


namespace snake_xyz.modules.rendering.console
{
    internal class ConsoleGameRenderer : IGameRenderer
    {
        public int Width => _curr.width;
        public int Height => _curr.height;

        private readonly ConsoleRenderer _buffer0;
        private readonly ConsoleRenderer _buffer1;

        private ConsoleRenderer _prev;
        private ConsoleRenderer _curr;

        private ConsoleColor[] _pallete;

        public ConsoleGameRenderer(ConsoleColor[] palette)
        {
            _buffer0 = new ConsoleRenderer(palette);
            _buffer1 = new ConsoleRenderer(palette);

            _buffer0.bgColor = ConsoleColor.Black;
            _buffer1.bgColor = ConsoleColor.Black;

            _prev = _buffer0;
            _curr = _buffer1;

            this._pallete = palette;
        }

        public void Clear()
        {
            _curr.Clear();
        }

        public void DrawFrame(GameState state)
        {
            foreach (var (x, y) in state.GetBody())
            {
                _curr.SetPixel(x, y, '■', 1);
            }

            var (ax, ay) = state.GetApple();
            _curr.SetPixel(ax, ay, '0', 2);

            //_curr.DrawString($"Level: {state.Level}", 2, 1, _pallete[3]);      
            //_curr.DrawString($"Score: {state.ApplesEaten}", 2, 2, _pallete[3]); 

            if (!_curr.Equals(_prev))
                _curr.Render();

            var tmp = _prev;
            _prev = _curr;
            _curr = tmp;
        }

        public void DrawTextScreen(string text)
        {
            _curr.Clear();

            var textHalfLength = text.Length / 2;
            var textY = _curr.height / 2;
            var textX = _curr.width / 2 - textHalfLength;

            _curr.DrawString(text, textX, textY, this._pallete[3]);

            if (!_curr.Equals(_prev))
                _curr.Render();

            var tmp = _prev;
            _prev = _curr;
            _curr = tmp;
        }
    }
}