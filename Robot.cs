
// Requirements:
// The library allows for a simulation of a toy robot moving on a 6 x 6 square tabletop.
// There are no obstructions on the table surface.
// The robot is free to roam around the surface of the table, but must be prevented from falling to destruction. Any movement that would result in this must be prevented, however further valid movement commands must still be allowed.
// PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.
// (0,0) can be considered as the SOUTH WEST corner and (5,5) as the NORTH EAST corner.
// The first valid command to the robot is a PLACE command. After that, any sequence of commands may be issued, in any order, including another PLACE command. The library should discard all commands in the sequence until a valid PLACE command has been executed.
// The PLACE command should be discarded if it places the robot outside of the table surface.
// Once the robot is on the table, subsequent PLACE commands could leave out the direction and only provide the coordinates. When this happens, the robot moves to the new coordinates without changing the direction.
// MOVE will move the toy robot one unit forward in the direction it is currently facing.
// LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position of the robot.
// REPORT will announce the X,Y and orientation of the robot.
// A robot that is not on the table can choose to ignore the MOVE, LEFT, RIGHT and REPORT commands.
namespace Robot
{
    /// <summary>
    /// Enum Direction defines 4 directions on the table.
    /// </summary>
    public enum Direction
    {
        // Define direction enum. Include None to initial robot before placing on the table.
        NONE = -1,
        NORTH = 0,
        EAST = 1,
        SOUTH = 2,
        WEST = 3
    }

    /// <summary>
    /// Class position defines 2D postion (X and Y) on the table
    /// </summary>
    public class Position
    {
        // Define position class with X and Y
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    /// <summary>
    /// Class Table defines size of table
    /// </summary>
    public class Table
    {
        // Define table class with table size
        public int X_Size { get; set; }
        public int Y_Size { get; set; }

        public Table(int x_size, int y_size)
        {
            this.X_Size = x_size;
            this.Y_Size = y_size;
        }
    }

    /// <summary>
    /// Class Rebot defines robot direction, position and table
    /// </summary>
    public class Robot
    {
        // current direction that robot facing
        private Direction direction;
        // current direction of robot
        private Position position;
        // defined table 
        private Table table;

        public Robot(Table table)
        {
            // initial robot with direction, position and table
            this.direction = Direction.NONE;
            this.position = new Position(-1, -1);
            this.table = table;
        }

        public override string ToString()
        {
            // Overide toString function to support Report
            return string.Format("{0},{1},{2}", position.X, position.Y, direction);
        }

        public void Report()
        {
            // check if position and direction are both valid, if not valid, skip the report command.
            // As per requirements discard all commands in the sequence until a valid PLACE command has been executed
            if (IsPosDirectionValid())
            {
                // Print out current position and direct.
                Console.WriteLine(this.ToString());
            }
        }

        public void Place(int x, int y, Direction direction)
        {
            // Place robot at certain position and direction.
            position.X = x;
            position.Y = y;
            this.direction = direction;
        }

        public void Place(int x, int y)
        {
            // Place robot at certain position. direction remains the same
            position.X = x;
            position.Y = y;
        }

        public bool IsPosDirectionValid(Position p, Direction d)
        {
            // Check if position and direction valid
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
            // Check if current robot's position and direction valid
            return IsPosDirectionValid(position, direction);
        }


        public void RotateLeft()
        {
            // Rotate left only when position and direction are both valid
            // As per requirements discard all commands in the sequence until a valid PLACE command has been executed
            if (IsPosDirectionValid())
            {
                // rotate left will rotate the robot 90 degrees to the left.
                // in the direction enum, rotate left equals to the previous direct. 
                // if no previous direct, set direction to the WEST
                direction = (int)direction - 1 == -1 ? Direction.WEST : direction - 1;
            }
        }

        public void RotateRight()
        {
            // Rotate right only when position and direction are both valid
            // As per requirements discard all commands in the sequence until a valid PLACE command has been executed
            if (IsPosDirectionValid())
            {
                // rotate right will rotate the robot 90 degrees to the right.
                // in the direction enum, rotate right equals to the next direct. 
                // if no next direct, set direction to the NORTH
                direction = (int)direction + 1 == 4 ? Direction.NORTH : direction + 1;
            }
        }

        public void Move()
        {
            // Move robot only when position and direction are both valid
            // As per requirements discard all commands in the sequence until a valid PLACE command has been executed
            if (IsPosDirectionValid())
            {
                // initial new position of robot with null, if new position remains null, it means current movement is not valid.
                Position? newPos = null;
                if (direction == Direction.NORTH)
                {
                    // Move to Noth -> Y + 1
                    newPos = new Position(position.X, position.Y + 1);
                }
                if (direction == Direction.EAST)
                {
                    // Move to EAST -> X + 1
                    newPos = new Position(position.X + 1, position.Y);
                }
                if (direction == Direction.WEST)
                {
                    // Move to WEST -> X - 1
                    newPos = new Position(position.X - 1, position.Y);
                }
                if (direction == Direction.SOUTH)
                {
                    // Move to SOUTH -> X - 1
                    newPos = new Position(position.X, position.Y - 1);
                }
                if (newPos != null && IsPosDirectionValid(newPos, direction))
                {
                    // Move robot to new position when new position is valid on the table.
                    this.position = newPos;
                }
            }

        }


        /// <summary>
        /// Run string command from console input. can be following commands
        ///  PLACE X, Y, DIRECTION
        ///  MOVE
        ///  LEFT
        ///  RIGHT
        ///  REPORT
        /// </summary>
        public void RunStringCommand(string command)
        {
            if (command != null)
            {
                // rotate left when command is LEFT
                if (command.Trim().Equals("LEFT"))
                {
                    this.RotateLeft();
                }
                // rotate right when command is RIGHT
                if (command.Trim().Equals("RIGHT"))
                {
                    this.RotateRight();
                }
                // move robot to toward current direction when command is MOVE
                if (command.Trim().Equals("MOVE"))
                {
                    this.Move();
                }
                // report current position and direction when command is REPORT
                if (command.Trim().Equals("REPORT"))
                {
                    this.Report();
                }
                // process PLACE command when command starts with PLACE
                if (command.Trim().StartsWith("PLACE"))
                {
                    // split PLACE command string with delimiter ',' and ' '
                    string[] comArray = command.Split(new Char[] { ',', ' ' });
                    // PLACE command can split into 4 or 3 length if array due to the following two type of PLACE command
                    // 1. PLACE 1,2,EAST -> place robot with X, Y and Direction. this will split into 4 length of array
                    //    ['PLACE', '1', '2', 'EAST']
                    // 2. PLACE 1,2 -> place robot with only X and Y. Direction remains the same. this will split into 3 length of array
                    //    ['PLACE', '1', '2']
                    if (comArray.Length == 4 || comArray.Length == 3)
                    {
                        int x = -1;
                        int y = -1;
                        Direction d = Direction.NONE;
                        if (!Int32.TryParse(comArray[1], out x) || !Int32.TryParse(comArray[2], out y))
                        {
                            // Try parse X and Y string to Int. if failed, do not procceed.
                            return;
                        }
                        if (comArray.Length == 4)
                        {
                            // Try parse direction string to direction enum. if failed, do not procceed.
                            if (!Enum.TryParse(comArray[3], out d))
                            {
                                return;
                            }
                        }
                        if (d == Direction.NONE)
                        {
                            // if place command do not contains direction, Place robot to new position.
                            this.Place(x, y);
                        }
                        else
                        {
                            // if place command contains direction, Place robot to new position and set direction.
                            this.Place(x, y, d);
                        }
                    }
                }
            }
        }
    }




}