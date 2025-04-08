using Game;

namespace Sn
{
    class Snake : GameObject
    {
        private int length;
        public enum Direction
        {
            up,
            down,
            left,
            right
        }
        private Direction snakeDirection;
        private int style;

        private int location_x, location_y; // Real-time location

        private List<Body> bodies = new List<Body>();

        public Snake(string name, int length, Direction snakeDirection, int style, int location_x, int location_y) : base(name)
        {
            this.length = length;
            this.snakeDirection = snakeDirection;
            this.style = style;

            this.location_x = location_x;
            this.location_y = location_y;
        }
        
        public int Location_x
        {
            get { return this.location_x;}
        }
        public int Location_y
        {
            get { return this.location_y;}
        }
        public void Move(ConsoleKeyInfo cki)
        {
            if(!(bodies == null || bodies.Count == 0))
            {
                // 记录蛇头移动前的位置
                int prevX = location_x;
                int prevY = location_y;
                // 依次保存每个节点的旧位置，并更新它为上一个节点旧的位置
                for (int i = 0; i < bodies.Count; i++)
                {
                    int tempX = bodies[i].Location_x;
                    int tempY = bodies[i].Location_y;

                    bodies[i].Location_x = prevX;
                    bodies[i].Location_y = prevY;

                    prevX = tempX;
                    prevY = tempY;
                }
            }

            switch (cki.Key)
            {
                case ConsoleKey.UpArrow:
                    this.snakeDirection = Direction.up;
                    this.location_y--;
                    break;
                case ConsoleKey.DownArrow:
                    this.snakeDirection = Direction.down;
                    this.location_y++;
                    break;
                case ConsoleKey.LeftArrow:
                    this.snakeDirection = Direction.left;
                    this.location_x--;
                    break;
                case ConsoleKey.RightArrow:
                    this.snakeDirection = Direction.right;
                    this.location_x++;
                    break;
            }
        }
        public void Grow(int num, int direction)
        {
            for(int i = 0; i < num; i++)
            {
                if(bodies.Count == 0)
                {
                    switch(direction)
                    {
                        case 0:
                        bodies.Add(new Body(location_x, location_y + 1));
                        break;
                        case 1:
                        bodies.Add(new Body(location_x, location_y - 1));
                        break;
                        case 2:
                        bodies.Add(new Body(location_x + 1, location_y));
                        break;
                        case 3:
                        bodies.Add(new Body(location_x - 1, location_y));
                        break;
                    }
                }
                else
                {
                    switch(direction)
                    {
                        case 0:
                        bodies.Add(new Body(bodies.Last().Location_x, bodies.Last().Location_y + 1));
                        break;
                        case 1:
                        bodies.Add(new Body(bodies.Last().Location_x, bodies.Last().Location_y - 1));
                        break;
                        case 2:
                        bodies.Add(new Body(bodies.Last().Location_x + 1, bodies.Last().Location_y));
                        break;
                        case 3:
                        bodies.Add(new Body(bodies.Last().Location_x - 1, bodies.Last().Location_y));
                        break;
                    }
                }
            }
        }
        public List<Body> GetBodies()
        {
            return bodies;
        }
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public Direction SnakeDirection
        {
            get { return snakeDirection; }
            set { snakeDirection = value; }
        }
        public int Style
        {
            get { return style; }
            set { style = value; }
        }
    }
    class Body
    {
        private int location_x, location_y; // Real-time location
        private int style = 3;

        public Body(int x, int y)
        {
            this.location_x = x;
            this.location_y = y;
        }
        public int Style
        {
            get { return style; }
        }
        public int Location_x
        {
            get { return location_x; }
            set { location_x = value; }
        }
        public int Location_y
        {
            get { return location_y; }
            set { location_y = value; }
        }
        public override string ToString()
        {
            return $"[{location_x},{location_y}]";
        }
    }
}