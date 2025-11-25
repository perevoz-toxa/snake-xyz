using snake_xyz.modules.input;
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

        public GameLogic()
        {
            this._gameState = new GameState();
            this._inputHandler = new InputHandler(this._gameState);
        }

        public void GoToGameplay()
        {
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
    }
}
