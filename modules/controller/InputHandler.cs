using snake_xyz.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.controller
{
    internal class InputHandler : IArrowHandler
    {
        private GameState _snake;

        public InputHandler(GameState snake) {
            _snake = snake;
        }
        public void OnArrowDown()
        {
            _snake.SetDirection(ESnakeDirection.Down);
        }

        public void OnArrowLeft()
        {
            _snake.SetDirection(ESnakeDirection.Left);
        }

        public void OnArrowRight()
        {
            _snake.SetDirection(ESnakeDirection.Right);
        }

        public void OnArrowUp()
        {
            _snake.SetDirection(ESnakeDirection.Up);
        }
    }
}
