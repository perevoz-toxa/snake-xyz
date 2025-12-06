using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.state
{
    internal class GameState
    {
        public bool IsAlive { get; private set; } = true;
        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }
        public int Level { get; set; } = 1;
        public int ApplesEaten { get; private set; }
        public (int x, int y) GetApple() => (_apple.x, _apple.y);

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
        private Cell _apple;
        private readonly Random _random = new();
        private List<Cell> _body = new();
        private ESnakeDirection currentDirection = ESnakeDirection.Left;
        private float _timeToMove = 0f;

        public void SetDirection(ESnakeDirection direction)
        {
            if (_body.Count <= 1)
            {
                currentDirection = direction;
                return;
            }

            var head = _body[0];
            var neck = _body[1];

            var next = ShiftTo(head, direction);

            if (next.x == neck.x && next.y == neck.y)
                return;
            currentDirection = direction;
        }

        public void Reset()
        {
            _body.Clear();
            currentDirection = ESnakeDirection.Right;
            _body.Add(new Cell(0, 0));
            _timeToMove = 0f;
            IsAlive = true;
            ApplesEaten = 0;

            GenerateApple();
        }

        public IEnumerable<(int x, int y)> GetBody()
        {
            foreach (var cell in _body)
                yield return (cell.x, cell.y);
        }

        public void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f || !IsAlive)
                return;

            _timeToMove = 1f / (4f + Level);
            var head = _body[0];
            var next = ShiftTo(head, currentDirection);

            if (fieldWidth > 0)
                next.x = (next.x + fieldWidth) % fieldWidth;
            if (fieldHeight > 0)
                next.y = (next.y + fieldHeight) % fieldHeight;

            foreach (var cell in _body)
            {
                if (cell.x == next.x && cell.y == next.y)
                {
                    IsAlive = false;
                    return;
                }
            }

            if (next.x == _apple.x && next.y == _apple.y)
            {
                _body.Insert(0, next); 
                ApplesEaten++;
                GenerateApple();
                return;
            }

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, next);
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

        private void GenerateApple()
        {
            Cell cell;
            do
            {
                cell = new Cell(_random.Next(fieldWidth), _random.Next(fieldHeight));
            } while (_body.Any(b => b.x == cell.x && b.y == cell.y));

            _apple = cell;
        }
    }
}
