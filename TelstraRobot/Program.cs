namespace TelstraRobot
{
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
                    robot.RunStringCommand(command);
                }
            }
        }
    }
}