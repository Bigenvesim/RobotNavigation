/*
filename: BreadthFirstSearch.cs
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

namespace RobotNavigation
{
    public class BreadthFirstSearch : Algorithm
    {
        public BreadthFirstSearch(Map map) : base(map)
        {

        }

        public override string Search(Agent robot)
        {
            Queue<Grid> queue = new Queue<Grid>();
            List<Grid> visitedGridList = new List<Grid>();
            Grid currentGrid;
            Grid goal = GoalPosition();
            Stopwatch.Start();
            // Check whether the initial state of the agent is the goal or not
            if (robot.X == goal.X && robot.Y == goal.Y)
            {
                Stopwatch.Stop();
                TimeSpent = Stopwatch.Elapsed.TotalSeconds.ToString();
                Solution = "Agent is already in the GOAL!!!";
            }
            else
            {
                // Add agent position to queue
                AgentPosition = new Point(robot.X, robot.Y);
                Grid agentGrid = new Grid(AgentPosition);
                queue.Enqueue(agentGrid);
                
                while (queue.Count != 0)
                {
                    currentGrid = queue.Dequeue();
                    Map.AddAdjacentGrid(currentGrid);

                    foreach (Path p in currentGrid.AdjecentGrids.ToList())
                    {
                        if (visitedGridList.Any(x => x.X == p.Location.X && x.Y == p.Location.Y))
                        {
                            currentGrid.AdjecentGrids.Remove(p);
                        }
                    }
                    visitedGridList.Add(currentGrid);
                    Gui.Draw(Map.MapLengthX, Map.MapLengthY, currentGrid);
                    foreach (Grid grid in Map.GridList)
                    {
                        //Check if the visited grid is in the map or not
                        if (currentGrid.X == grid.X && currentGrid.Y == grid.Y)
                        {
                            // Check if the agent has reached the goal or not
                            if (currentGrid.X == goal.X && currentGrid.Y == goal.Y)
                            {
                                Stopwatch.Stop();
                                TimeSpent = Stopwatch.Elapsed.TotalSeconds.ToString();
                                return Solution = OutputSolution(robot, "BFS", goal, visitedGridList);
                            }
                            else
                            {
                                //Check if there are any adjacent grids
                                if (currentGrid.AdjecentGrids.Count != 0)
                                {
                                    foreach (Path path in currentGrid.AdjecentGrids)
                                    {
                                        // Check if that adjacent grid is alredy visited or not. if not, then move to it
                                        if ((!visitedGridList.Any(x => x.X == path.Location.X && x.Y == path.Location.Y)) && !queue.Any(x => x.X == path.Location.X && x.Y == path.Location.Y))
                                        {
                                            path.Location.ParentGrid = currentGrid;
                                            queue.Enqueue(path.Location);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Stopwatch.Stop();
                TimeSpent = Stopwatch.Elapsed.TotalSeconds.ToString();
            }
            return Solution;
        }
    }
}
