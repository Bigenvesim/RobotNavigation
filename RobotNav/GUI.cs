/*
filename: GUI.cs
author: Pham Phuc Hung Le (101985894)
created: 14/04/2019
last modified: 25/04/2019
description: The GUI class is made for Drawing GUI of the program
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SwinGameSDK;

namespace RobotNavigation
{
    public class GUI
    {
        private Map _map = new Map();
     
        public GUI(Map map)
        {
            SwinGame.LoadBitmapNamed("up", "up.png");
            SwinGame.LoadBitmapNamed("left", "left.png");
            SwinGame.LoadBitmapNamed("down", "down.png");
            SwinGame.LoadBitmapNamed("right", "right.png");
            _map = map;
        }

        // Draw Grid
        public void DrawGrid(Color color, int x, int y)
        {
            SwinGame.DrawRectangle(Color.Black, x, y, 100, 100);
            SwinGame.FillRectangle(color, x + 1, y + 1, 98, 98);
        }
        
        // Draw changes of agent state
        public void Draw(int mapLengthX, int mapLengthY, Grid currentGrid)
        {
            SwinGame.ClearScreen(Color.White);
            for (int i = 0; i < mapLengthX*100; i+=100)
            {
                SwinGame.ProcessEvents();
                for (int j = 0; j < mapLengthY*100; j+=100)
                {
                    SwinGame.ProcessEvents();
                    DrawGrid(Color.White, i, j);
                    foreach (Grid g in _map.GridList)
                    {
                        SwinGame.ProcessEvents();
                        if (g.GridName == "ROBOT")
                        {
                            DrawGrid(Color.Red, g.X * 100, g.Y * 100);
                        }
                        if (g.GridName == "WALL")
                        {
                            DrawGrid(Color.Grey, g.X * 100, g.Y * 100);
                        }
                        if (g.GridName == "GOAL")
                        {
                            DrawGrid(Color.Green, g.X * 100, g.Y * 100);
                        }
                    }
                }
            }
            SwinGame.RefreshScreen(60);
            for (int i = 0; i < mapLengthX * 100; i += 100)
            {
                SwinGame.ProcessEvents();
                for (int j = 0; j < mapLengthY * 100; j += 100)
                {
                    SwinGame.ProcessEvents();
                    foreach (Grid g in _map.GridList)
                    {
                       SwinGame.ProcessEvents();
                       if(g.X == currentGrid.X && g.Y == currentGrid.Y)
                       {
                            DrawGrid(Color.Pink, g.X * 100, g.Y * 100);
                            SwinGame.RefreshScreen(0);
                       } 
                    }
                }
            }
        }

        // Draw solution Path to the GUI
        public void DrawSolution(int mapLengthX, int mapLengthY, List<Grid> solutionPaths)
        {
            SwinGame.ClearScreen(Color.White);
            SwinGame.ProcessEvents();
            for (int i = 0; i < mapLengthX * 100; i += 100)
            {
                SwinGame.ProcessEvents();
                for (int j = 0; j < mapLengthY * 100; j += 100)
                {
                    SwinGame.ProcessEvents();
                    DrawGrid(Color.White, i, j);
                    foreach (Grid g in solutionPaths)
                    {
                        DrawGrid(Color.Pink, g.X * 100, g.Y * 100);
                    }
                }
            }
            for (int i = 0; i < mapLengthX * 100; i += 100)
            {
                SwinGame.ProcessEvents();
                for (int j = 0; j < mapLengthY * 100; j += 100)
                {
                    SwinGame.ProcessEvents();
                    foreach (Grid g in _map.GridList)
                    {
                        SwinGame.ProcessEvents();
                        if (g.GridName == "ROBOT")
                        {
                            DrawGrid(Color.Red, g.X * 100, g.Y * 100);
                        }
                        if (g.GridName == "WALL")
                        {
                            DrawGrid(Color.Grey, g.X * 100, g.Y * 100);
                        }
                        if (g.GridName == "GOAL")
                        {
                            DrawGrid(Color.Green, g.X * 100, g.Y * 100);
                        }
                    }
                }
            }
            for (int i = 0; i < mapLengthX * 100; i += 100)
            {
                SwinGame.ProcessEvents();
                for (int j = 0; j < mapLengthY * 100; j += 100)
                {
                    SwinGame.ProcessEvents();
                    foreach (Grid g in solutionPaths)
                    {
                        DrawAction(g.MoveDirection, g.X*100, g.Y*100);
                    }
                }
            }
            SwinGame.RefreshScreen(60);
        }

        // Draw the moving direction of agent
        public void DrawAction(string direction, int x, int y)
        {
            SwinGame.ProcessEvents();

            if (direction == "up")
            {
                SwinGame.DrawBitmap("up", x + 32, y + 35);
            }
            if (direction == "left")
            {
                SwinGame.DrawBitmap("left", x + 32, y + 35);
            }
            if (direction == "down")
            {
                SwinGame.DrawBitmap("down", x + 32, y + 35);
            }
            if (direction == "right")
            {
                SwinGame.DrawBitmap("right", x + 32, y + 35);
            }
        }
    }
}

