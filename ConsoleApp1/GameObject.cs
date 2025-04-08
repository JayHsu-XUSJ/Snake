using Runtime;

namespace Game
{
    class GameObject
    {
        private string name;
        public GameObject(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return name; }
        }
    }
    class MapManager
    {
        private int width, height;
        private int[,] map;

        private int coinNum = 5; // 硬币数量
        public enum MapItem
        {
            air,
            wall,
            coin,
            body,
            head
        }
        Dictionary<int, char> styles = new Dictionary<int, char>();
        public char GetStyleChar(int style)
        {
            return styles[style];
        }
        
        public MapManager(int width, int height)
        {
            styles[(int)MapItem.air] = ' ';
            styles[(int)MapItem.wall] = '▓';
            styles[(int)MapItem.coin] = '*';
            styles[(int)MapItem.body] = 'X';
            styles[(int)MapItem.head] = 'O';

            
            this.width = width;
            this.height = height;
            this.map = new int[height, width];

            // Initialize the map.
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if(i == 0 || i == height - 1)
                        map[i, j] = (int)MapItem.wall;
                    else if(j == 0 || j == width - 1)
                        map[i, j] = (int)MapItem.wall;
                    else map[i, j] = (int)MapItem.air;
                }
            }
            CreateCoin(height, width, coinNum);

            
        }
        public void CreateCoin(int width, int height, int num)
        {
            Random rd = new Random();
            for(int i = 0; i < num; i++)
            {
                int x = rd.Next(1, width - 1);
                int y = rd.Next(1, height - 1);
                PutItem(2, y, x);
            }
        }
        public int DisplayMap()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Console.Write(styles[map[i, j]]);
                }
                Console.Write(Environment.NewLine);
            }
            GamePlay.PrintInstruction();
            return 0;
        }
        public int PutItem(int item, int x, int y)
        {
            if((x <= 0) || (x >= width - 1) || (y <= 0) || (y >= height - 1))
                return -1; // Hitting the wall

            if(map[y, x] == (int)MapItem.coin)
            {
                map[y, x] = (int)MapItem.air;
                coinNum--;
                if(coinNum <= 0)
                {
                    coinNum = 5;
                    CreateCoin(height, width, 5);
                    DisplayMap();
                }
                return 888; // Ate the coin.
            }

            map[y, x] = item;
            return 0; 
        }
    }

    class GUI
    {
        public void DisplayKey(ConsoleKeyInfo cki)
        {
            Console.SetCursorPosition(0, 27);
            switch (cki.Key)
            {
                case ConsoleKey.UpArrow:
                    Console.Write("↑");
                    break;
                case ConsoleKey.DownArrow:
                    Console.Write("↓");
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Write("←");
                    break;
                case ConsoleKey.RightArrow:
                    Console.Write("→");
                    break;
                default:
                    Console.Write(" ");
                    break;
            }
        }

        public void DisplayScoreBoard(int x, int y, int score, int length)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("-----------");
            Console.SetCursorPosition(x, y + 1);
            Console.Write($"***| {score} |***");
            Console.SetCursorPosition(x, y + 2);
            Console.Write($"LENGTH:{length}   ");
            Console.SetCursorPosition(x, y + 3);
            Console.Write("-----------");
        }
        public void DisplayGG(int score, int length)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"GAMEOVER...    SCORE:{score} LENGTH:{length}");
            Console.ReadLine();
        }
        public void DisplayStartMenu()
        {
            int sign = 1;
            const int max_sign = 1;

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("→ NEW GAME");
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("  EXIT");

            while (true)
            {

                Console.SetCursorPosition(0, 2);
                Console.Write(sign);




                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    sign++;
                    if(sign > max_sign) sign = max_sign;
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("→ NEW GAME");

                    Console.SetCursorPosition(0, 1);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("  EXIT");
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    sign--;
                    if(sign < 0) sign = 0;
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("  NEW GAME");

                    Console.SetCursorPosition(0, 1);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("→ EXIT");
                }
                else if(cki.Key == ConsoleKey.Enter)
                {
                    switch(sign)
                    {
                        case 1:
                        goto GameStart;
                        case 0:
                        Environment.Exit(0);
                        break;
                    }
                }
            }

        GameStart:
        Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}