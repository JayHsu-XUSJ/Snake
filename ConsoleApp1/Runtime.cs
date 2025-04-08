using Game;
using Sn;

namespace Runtime
{
    class GamePlay
    {
        public static int PrintInstruction()
        {
            Console.SetCursorPosition(0, 26);
            Console.WriteLine($"P1: ↑ ↓ ← →");
            return 0;
        }
        public static void Main(string[] args)
        {   
            RE_START:
            const int mapWidth = 100, mapHeight = 25;

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Gray;

            MapManager mapManager = new MapManager(mapWidth, mapHeight);
            Snake snake = new("Eric", 1, Snake.Direction.up, 4, 50, 12);
            ConsoleKeyInfo cki = new ConsoleKeyInfo('\0', ConsoleKey.UpArrow, false, false, false);
            GUI gui = new();

            int collideStatus, score = 0, bufferCount;

            gui.DisplayStartMenu();

            mapManager.DisplayMap();

            while (true)
            {
                bufferCount = 1;
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    bufferCount += 2;
                }
                if(bufferCount >= 2)
                    bufferCount--; // 多了一次，所以减去

                if(bufferCount == 1)
                    Thread.Sleep(500);
                else
                {
                    Thread.Sleep(100);
                }

                if (Console.KeyAvailable)
                    cki = Console.ReadKey(true);



                for (int i = 0; i < bufferCount; i++)
                {
                    // Clear the last char of the snake. 
                    mapManager.PutItem(0, snake.Location_x, snake.Location_y);
                    Console.SetCursorPosition(snake.Location_x, snake.Location_y);
                    Console.Write(" ");
                    foreach (var body in snake.GetBodies())
                    {
                        mapManager.PutItem(0, body.Location_x, body.Location_y);
                        Console.SetCursorPosition(body.Location_x, body.Location_y);
                        Console.Write(" "); // 覆盖打印旧的身体
                    }

                    snake.Move(cki);
                    collideStatus = mapManager.PutItem(snake.Style, snake.Location_x, snake.Location_y);

                    if (collideStatus == -1)
                    {
                        goto GAMEOVER;
                    }
                    else if (collideStatus == 888)
                    {
                        snake.Grow(1, (int)snake.SnakeDirection);
                        snake.Length += 1;
                        score += 1;
                    }

                    // refresh
                    Console.SetCursorPosition(snake.Location_x, snake.Location_y);
                    Console.Write(mapManager.GetStyleChar(snake.Style)); // Print head
                    foreach (var body in snake.GetBodies())
                    {
                        Console.SetCursorPosition(body.Location_x, body.Location_y);
                        Console.Write(mapManager.GetStyleChar(body.Style)); // Print body
                    }
                }

                gui.DisplayKey(cki);
                gui.DisplayScoreBoard(mapWidth + 1, mapHeight, score, snake.Length);
            }
            
            GAMEOVER:
            gui.DisplayGG(score, snake.Length);
            goto RE_START;
        }
    }
}