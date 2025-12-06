using snake_xyz.modules.controller;
using snake_xyz.modules.input;
using snake_xyz.modules.rendering;
using snake_xyz.modules.rendering.console;
using System.Text;

namespace snake_xyz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Input input = new ConsoleInput();
            var palette = new[] { ConsoleColor.Black, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.White };
            IGameRenderer renderer = new ConsoleGameRenderer(palette);
            GameLogic gameLogic = new GameLogic(renderer);
            gameLogic.InitializeInput(input);
            gameLogic.GoToGameplay();

            var lastFrameTime = DateTime.Now;
            while (true)
            {
                input.Update();
                var frameStartTime = DateTime.Now;
                float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                gameLogic.DrawNewState(deltaTime);
                lastFrameTime = frameStartTime;
            }
        }
    }
}
