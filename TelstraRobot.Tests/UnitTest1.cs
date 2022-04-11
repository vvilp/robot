using NUnit.Framework;
using TelstraRobot;
using System.IO;
using System;
namespace TelstraRobot.Tests;

public class Tests
{
    private Robot robot;

    [SetUp]
    public void Setup()
    {
        Table table = new Table(6, 6);
        robot = new Robot(table);
    }

    [Test]
    public void Test1()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 0,0,NORTH");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("REPORT");
            string expected = "0,1,NORTH\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test2()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 0,0,NORTH");
            robot.RunStringCommand("LEFT");
            robot.RunStringCommand("REPORT");
            string expected = "0,0,WEST\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test3()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1,2,EAST");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("LEFT");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("REPORT");
            string expected = "3,3,NORTH\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test4()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1,2,EAST");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("LEFT");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("PLACE 3,1");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("REPORT");
            string expected = "3,2,NORTH\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test5()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("REPORT");
            // did not place robot at correct place at begin. ignore these two commands
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test6()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1x,2x,EAST");
            robot.RunStringCommand("REPORT");
            // did not give correct PLACE command at begin. ignore these two commands
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test7()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 10,10");
            robot.RunStringCommand("REPORT");
            // did not place robot at correct place at begin. ignore next commands
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test8()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 5,5,WEST");
            robot.RunStringCommand("REPORT");
            string expected = "5,5,WEST\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test9()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 5,5,EAST");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("REPORT");
            string expected = "5,5,EAST\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test10()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 5,5,EAST");
            robot.RunStringCommand("RIGHT");
            robot.RunStringCommand("MOVE");
            robot.RunStringCommand("REPORT");
            string expected = "5,4,SOUTH\n";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test11()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1,2");
            robot.RunStringCommand("REPORT");
            // did not give correct PLACE command with direction at begin. ignore other command
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test12()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1,2x,NORTH");
            robot.RunStringCommand("REPORT");
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test13()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1,NORTH");
            robot.RunStringCommand("REPORT");
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test14()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1");
            robot.RunStringCommand("REPORT");
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }

    [Test]
    public void Test15()
    {
        // redirect console output to String writer for testing
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.RunStringCommand("PLACE 1,2");
            robot.RunStringCommand("REPORT");
            string expected = "";
            Assert.AreEqual(expected, sw.ToString());
        }
    }
}