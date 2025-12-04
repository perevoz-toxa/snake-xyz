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

        public GameLogic(IGameRenderer renderer)
        {
            this._gameState = new GameState();
            this._inputHandler = new InputHandler(this._gameState);
            this._renderer = renderer;
        }

        public void GoToGameplay()
        {
            _gameState.Reset();
            _gameState.fieldWidth = 20;
            _gameState.fieldHeight = 10;
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
            if (!_gameState.IsAlive)
            {
                // "Game Over" 
                _renderer.Clear();
                _renderer.DrawFrame(_gameState);
                return;
            }

            Update(deltaTime);
            _renderer.Clear();
            _renderer.DrawFrame(_gameState);
        }
    }
}
