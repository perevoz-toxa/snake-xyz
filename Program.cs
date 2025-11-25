using snake_xyz.modules.controller;
using snake_xyz.modules.input;
using System.Text;

namespace snake_xyz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Input input = new ConsoleInput();
            GameLogic gameLogic = new GameLogic();
            gameLogic.InitializeInput(input);

            gameLogic.GoToGameplay();

            var lastFrameTime = DateTime.Now;
            while (true)
            {
                input.Update();
                var frameStartTime = DateTime.Now;
                float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                gameLogic.Update(deltaTime);
                lastFrameTime = frameStartTime;
            }
        }
    }
}
