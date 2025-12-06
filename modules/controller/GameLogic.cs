using snake_xyz.modules.input;
using snake_xyz.modules.rendering;
using snake_xyz.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_xyz.modules.controller
{
    internal class GameLogic
    {
        private GameState _gameState;
        private InputHandler _inputHandler;
        private IGameRenderer _renderer;
        private int _level = 1;
        private bool _showingLevelText = true;
        private float _levelTextTimeLeft = 2f;
        private bool _showingGameOver = false;
        private float _gameOverTimeLeft = 2f;

        public GameLogic(IGameRenderer renderer)
        {
            this._gameState = new GameState();
            this._inputHandler = new InputHandler(this._gameState);
            this._renderer = renderer;
        }

        public void GoToGameplay()
        {
            _gameState.Level = _level;
            _gameState.fieldWidth = _renderer.Width;
            _gameState.fieldHeight = _renderer.Height;
            _gameState.Reset();
        }

        public void InitializeInput(Input input)
        {
            input.Subscribe(this._inputHandler);
        }
        public void Update(float deltaTime)
        {
            _gameState.Update(deltaTime);
        }

        public void DrawNewState(float deltaTime)
        {
            _gameState.fieldWidth = _renderer.Width;
            _gameState.fieldHeight = _renderer.Height;
            _gameState.Level = _level;

            if (_showingGameOver)
            {
                _gameOverTimeLeft -= deltaTime;

                _renderer.Clear();
                _renderer.DrawTextScreen("Game Over!");

                if (_gameOverTimeLeft <= 0f)
                {
                    _showingGameOver = false;

                    _level = 1;
                    _showingLevelText = true;
                    _levelTextTimeLeft = 2f;

                    GoToGameplay();
                }

                return;
            }

            if (_showingLevelText)
            {
                _levelTextTimeLeft -= deltaTime;
                _renderer.Clear();
                _renderer.DrawTextScreen($"Level {_level}");

                if (_levelTextTimeLeft <= 0f)
                {
                    _showingLevelText = false;
                }

                return;
            }

            Update(deltaTime);

            if (!_gameState.IsAlive)
            {
                _showingGameOver = true;
                _gameOverTimeLeft = 2f;

                _renderer.Clear();
                _renderer.DrawTextScreen("Game Over!");
                return;
            }

            if (_gameState.ApplesEaten >= _level * 3)
            {
                _level++;
                _showingLevelText = true;
                _levelTextTimeLeft = 2f;
                GoToGameplay();
                return;
            }

            _renderer.Clear();
            _renderer.DrawFrame(_gameState);
        }
    }
}
