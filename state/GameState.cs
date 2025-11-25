using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.state
{
    internal class GameState
    {
        private struct Cell
        {
            public int x;
            public int y;

            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private List<Cell> _body = new();
        private ESnakeDirection currentDirection = ESnakeDirection.Left;

        private float _timeToMove = 0f;

        public void SetDirection(ESnakeDirection direction)
        {
            currentDirection = direction;
        }

        public void Reset()
        {
            _body.Clear();
            currentDirection = ESnakeDirection.Left;
            _body.Add(new Cell(0, 0));
            _timeToMove = 0f;
        }

        public void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;

            _timeToMove = 1f / 4;
            var head = _body[0];
            var nextCell = ShiftTo(head, currentDirection);

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);

            string directionToPring = "";
            switch (currentDirection)
            {
                case ESnakeDirection.Left:
                    directionToPring = "←";
                    break;
                case ESnakeDirection.Right:
                    directionToPring = "→";
                    break;
                case ESnakeDirection.Up:
                    directionToPring = "↑";
                    break;
                case ESnakeDirection.Down:
                    directionToPring = "↓";
                    break;
            }
            Console.WriteLine($"{_body[0].x}, {_body[0].y}, {directionToPring}");
        }

        private Cell ShiftTo(Cell from, ESnakeDirection toDirection)
        {
            switch (toDirection)
            {
                case ESnakeDirection.Left:
                    return new Cell(from.x - 1, from.y);
                case ESnakeDirection.Right:
                    return new Cell(from.x + 1, from.y);
                case ESnakeDirection.Up:
                    return new Cell(from.x, from.y - 1);
                case ESnakeDirection.Down:
                    return new Cell(from.x, from.y + 1);
            }

            return from;
        }
    }
}
