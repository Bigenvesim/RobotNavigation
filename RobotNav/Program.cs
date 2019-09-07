/*
filename: Program.cs
author: Pham Phuc Hung Le (101985894)
created: 14/04/2019
last modified: 25/04/2019
description: 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using SwinGameSDK;

namespace RobotNavigation
{
    public class Program
    {
        static void Main(string[] args)
        {

            string link = args[0];
            Map map = new Map();
            Agent robot;
            map.Data.Link = link;
            map.GenerateMap();
            robot = new Agent(map);
            Algorithm dfs = new DepthFirstSearch(map);
            Algorithm bfs = new BreadthFirstSearch(map);
            Algorithm gbfs = new GreedyBestFirstSearch(map);
            Algorithm astart = new AStar(map);
            Algorithm cus1 = new DijkstraSearch(map);
            Algorithm cus2 = new CUS2(map);
            Algorithm as1 = new AstarWithMoveDirectionCost(map);

            SwinGame.OpenGraphicsWindow("RobotNavigation", map.MapLengthX*100, map.MapLengthY*100);

            SwinGame.ClearScreen(Color.White);
            while (false == SwinGame.WindowCloseRequested())
            {
                SwinGame.ProcessEvents();
                switch (args[1].ToLower())
                {
                    case "dfs":
                        Console.WriteLine(dfs.Search(robot));
                        break;
                    case "bfs":
                        Console.WriteLine(bfs.Search(robot));
                        break;
                    case "gbfs":
                        Console.WriteLine(gbfs.Search(robot));
                        break;
                    case "as":
                        Console.WriteLine(astart.Search(robot));
                        break;
                    case "as1":
                        Console.WriteLine(as1.Search(robot));
                        break;
                    case "cus1":
                        Console.WriteLine(cus1.Search(robot));
                        break;
                    case "cus2":
                        Console.WriteLine(cus2.Search(robot));
                        break;
                    default:
                        Console.WriteLine("You haven't chose the method. Please choose one!");
                        break;
                }
                break;
            }
            Console.ReadLine();
        }
    }
}
