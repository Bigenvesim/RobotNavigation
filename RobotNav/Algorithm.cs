/*
filename: Algorithm.cs
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
using System.Threading;
using System.Diagnostics;

namespace RobotNavigation
{
    public abstract class Algorithm
    {
        // Each method will return the solution in string and that string will be printed out to the Console.
        private string _solution = "No solution found";
        private Point _agentPosition;
        private Map _map = new Map();
        private List<Grid> _solutionPaths = new List<Grid>();
        private int _numberofCheckedGrids;
        private string _timeSpent;
        private Stopwatch _stopwatch = new Stopwatch();
        private int _numberofStep;
        private GUI _gui;

        public Map Map { get => _map; set => _map = value; }
        public List<Grid> SolutionPaths { get => _solutionPaths; set => _solutionPaths = value; }
        public Point AgentPosition { get => _agentPosition; set => _agentPosition = value; }
        public string Solution { get => _solution; set => _solution = value; }
        // These properties are used for showing the data of each algorithm
        public string TimeSpent { get => _timeSpent; set => _timeSpent = value; }
        public Stopwatch Stopwatch { get => _stopwatch; set => _stopwatch = value; }
        public int NumberofCheckedGrids { get => _numberofCheckedGrids; set => _numberofCheckedGrids = value; }
        public int NumberofStep { get => _numberofStep; set => _numberofStep = value; }
        public GUI Gui { get => _gui; set => _gui = value; }

        public Algorithm (Map map)
        {
            _map = map;
            _gui = new GUI(map);
        }

        public Grid GoalPosition()
        {
            Grid goalPosition = null;
            foreach (Grid grid in Map.GridList)
            {
                if (grid.GridName == "GOAL")
                {
                    Point pt = new Point(grid.X, grid.Y);
                    goalPosition = new Grid(pt);
                    return goalPosition;
                }
            }
            return goalPosition;
        }

        public abstract string Search(Agent robot);

        public void AnalyseData(List<Grid> visitedGrids, string methodName)//Algorithm method, List<Grid> visitedGrids)
        {
            NumberofCheckedGrids = visitedGrids.Count() - 1; //minus Initial grid
            
            Console.WriteLine("{0} algorithm", methodName);
            Console.WriteLine("Time to solve: {0} second", TimeSpent);
            Console.WriteLine("Checked {0} Grids", NumberofCheckedGrids);
            Console.WriteLine("There are {0} steps long to the goal", NumberofStep);
            Console.WriteLine("---------------");
            Console.WriteLine("{0}  {1}  {2}", Map.Data.Link, methodName, Map.GridList.Count);
            
        }

        // Calculate Mannhattan Distance
        public double CalManhattanDistance (int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        // Output Solution as a string
        public string OutputSolution(Agent robot, string method, Grid goal, List<Grid> visitedGrids)
        {
            string solution = "> ";
            List<Grid> solutionPaths = new List<Grid>();
            List<string> moveList = new List<string>();

            solutionPaths = ProduceSolutionPaths(goal, visitedGrids);
           
            //Produce action from path
            for (int i = 0; i < solutionPaths.Count(); i++)
            {
                if (i == solutionPaths.Count() - 1)
                {
                    break;
                }
                if (solutionPaths[i + 1].Y == solutionPaths[i].Y - 1)
                {
                    solutionPaths[i].MoveDirection = "up";
                    moveList.Add(robot.MoveUp());
                }
                if (solutionPaths[i + 1].X == solutionPaths[i].X - 1)
                {
                    solutionPaths[i].MoveDirection = "left";
                    moveList.Add(robot.MoveLeft());
                }
                if (solutionPaths[i + 1].Y == solutionPaths[i].Y + 1)
                {
                    solutionPaths[i].MoveDirection = "down";
                    moveList.Add(robot.MoveDown());
                }
                if (solutionPaths[i + 1].X == solutionPaths[i].X + 1)
                {
                    solutionPaths[i].MoveDirection = "right";
                    moveList.Add(robot.MoveRight());
                }
            }

            foreach (string move in moveList)
            {
                solution = solution + move + "; ";
            }
            NumberofStep = moveList.Count;
            AnalyseData(visitedGrids, method);
            _gui.DrawSolution(_map.MapLengthX, _map.MapLengthY, solutionPaths);
            //Console.WriteLine(solution);
            //Thread.Sleep(50000000);
            return solution;
        }

        // Produce solution paths from visited grids
       public List<Grid> ProduceSolutionPaths (Grid goal, List<Grid> visitedGrids)
       {
            List<Grid> solutionPaths = new List<Grid>();
            visitedGrids.Reverse();

            foreach (Grid grid in visitedGrids)
            {
                if (grid.X == goal.X && grid.Y == goal.Y)
                {
                    solutionPaths.Add(grid);
                }
                if (solutionPaths.Count() != 0)
                {
                    if (grid.X == solutionPaths.Last().ParentGrid.X  && grid.Y == solutionPaths.Last().ParentGrid.Y)
                    {
                        solutionPaths.Add(grid);
                    }
                }
            }
            solutionPaths.Reverse();

            return solutionPaths;
       }
    }
}



