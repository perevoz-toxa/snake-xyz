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
            currentDirection = ESnakeDirection.Right;
            _body.Add(new Cell(0, 0));
            _timeToMove = 0f;
            IsAlive = true;
        }

        public IEnumerable<(int x, int y)> GetBody()
        {
            foreach (var cell in _body)
                yield return (cell.x, cell.y);
        }

        public void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;

            _timeToMove = 1f / 4;
            var head = _body[0];
            var next = ShiftTo(head, currentDirection);

            if (next.x < 0 || next.x >= fieldWidth ||
                next.y < 0 || next.y >= fieldHeight)
            {
                IsAlive = false;
                return;
            }
            var nextCell = ShiftTo(head, currentDirection);

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
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
