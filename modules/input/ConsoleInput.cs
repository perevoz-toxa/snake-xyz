using snake_xyz.modules.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.input
{
    internal class ConsoleInput : Input
    {
        private readonly HashSet<IArrowHandler> _arrowListeners = new();

        public override void Subscribe(IArrowHandler listener)
        {
            _arrowListeners.Add(listener);
        }

        public override void Update()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow or ConsoleKey.W:
                        foreach (var l in _arrowListeners) l.OnArrowUp();
                        break;
                    case ConsoleKey.DownArrow or ConsoleKey.S:
                        foreach (var l in _arrowListeners) l.OnArrowDown();
                        break;
                    case ConsoleKey.LeftArrow or ConsoleKey.A:
                        foreach (var l in _arrowListeners) l.OnArrowLeft();
                        break;
                    case ConsoleKey.RightArrow or ConsoleKey.D:
                        foreach (var l in _arrowListeners) l.OnArrowRight();
                        break;
                }
            }

        }
    }
}
