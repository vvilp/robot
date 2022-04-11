namespace Robot
{
    public enum Direction
    {
        NONE = -1,
        NORTH = 0,
        EAST = 1,
        SOUTH = 2,
        WEST = 3
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Table
    {
        public int X_Size { get; set; }
        public int Y_Size { get; set; }

        public Table(int x_size, int y_size)
        {
            this.X_Size = x_size;
            this.Y_Size = y_size;
        }
    }

    public class Robot
    {
        private Direction direction;
        private Position position;
        private Table table;

        public Robot(Table table)
        {
            this.direction = Direction.NONE;
            this.position = new Position(-1, -1);
            this.table = table;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", position.X, position.Y, direction);
        }

        public void Report()
        {
            if (IsPosDirectionValid())
            {
                Console.WriteLine(this.ToString());
            }
        }

        public void Place(int x, int y, Direction direction)
        {
            position.X = x;
            position.Y = y;
            this.direction = direction;
        }

        public void Place(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public bool IsPosDirectionValid(Position p, Direction d)
        {
            if (p.X >= table.X_Size || p.X < 0 || p.Y >= table.Y_Size || p.Y < 0 || d == Direction.NONE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsPosDirectionValid()
        {
            return IsPosDirectionValid(position, direction);
        }


        public void RotateLeft()
        {
            if (IsPosDirectionValid())
            {
                direction = (int)direction - 1 == -1 ? Direction.WEST : direction - 1;
            }
        }

        public void RotateRight()
        {
            if (IsPosDirectionValid())
            {
                direction = (int)direction + 1 == 4 ? Direction.NORTH : direction + 1;
            }
        }

        public void Move()
        {
            if (!IsPosDirectionValid())
            {
                return;
            }
            Position? newPos = null;
            if (direction == Direction.NORTH)
            {
                newPos = new Position(position.X, position.Y + 1);
            }
            if (direction == Direction.EAST)
            {
                newPos = new Position(position.X + 1, position.Y);
            }
            if (direction == Direction.WEST)
            {
                newPos = new Position(position.X - 1, position.Y);
            }
            if (direction == Direction.SOUTH)
            {
                newPos = new Position(position.X, position.Y - 1);
            }
            if (newPos != null && IsPosDirectionValid(newPos, direction))
            {
                this.position = newPos;
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(6, 6);
            Robot robot = new Robot(table);

            while (true)
            {
                string? command = Console.ReadLine();
                if (command != null)
                {
                    if (command.Trim().Equals("LEFT"))
                    {
                        robot.RotateLeft();
                    }
                    if (command.Trim().Equals("RIGHT"))
                    {
                        robot.RotateRight();
                    }
                    if (command.Trim().Equals("MOVE"))
                    {
                        robot.Move();
                    }
                    if (command.Trim().Equals("REPORT"))
                    {
                        robot.Report();
                    }
                    if (command.Trim().StartsWith("PLACE"))
                    {
                        string[] comArray = command.Split(new Char[] { ',', ' ' });
                        if (comArray.Length == 4 || comArray.Length == 3)
                        {
                            int x = -1;
                            int y = -1;
                            Direction d = Direction.NONE;
                            if (!Int32.TryParse(comArray[1], out x) || !Int32.TryParse(comArray[2], out y))
                            {
                                continue;
                            }
                            if (comArray.Length == 4)
                            {
                                if (!Enum.TryParse(comArray[3], out d))
                                {
                                    continue;
                                }
                            }
                            // Console.WriteLine($"{x}, {y}, {d}");
                            if (d == Direction.NONE)
                            {
                                robot.Place(x, y);

                            }
                            else
                            {
                                robot.Place(x, y, d);
                            }
                        }
                    }
                }
            }
        }
    }
}