/*
filename: Map.cs
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
    public class Map
    {
        private List<Grid> _wallList = new List<Grid>();
        private List<Grid> _gridList = new List<Grid>();
        private List<Path> _pathList = new List<Path>();
        private int _robotX;
        private int _robotY;
        private List<string> _dataList = new List<string>();
        private int _mapLengthX;
        private int _mapLengthY;
        private Resources _data = new Resources();

        public void GenerateMap()
        {
            _dataList = _data.LoadDataFromFile();
            int lineNumber = 0;
            foreach (string data in _dataList)
            {
                string[] text = new string[6];
                text = data.Split(' ');
                if (lineNumber == 0)
                {
                    _mapLengthX = Int32.Parse(text[2]);
                    _mapLengthY = Int32.Parse(text[1]);

                    AddGrids(_mapLengthY, _mapLengthX);
                }
                else if (lineNumber == 1)
                {
                    _robotX = Int32.Parse(text[1]); 
                    _robotY = Int32.Parse(text[2]);
                    foreach (Grid grid in _gridList)
                    {
                        if (_robotX == grid.X && _robotY == grid.Y)
                        {
                            grid.GridName = "ROBOT";
                        }

                    }
                }
                else if (lineNumber == 2)
                {
                    AddGoal(Int32.Parse(text[1]), Int32.Parse(text[2]));
                }
                else if ((lineNumber >= 3))
                {
                    AddWalls(Int32.Parse(text[1]), Int32.Parse(text[2]), Int32.Parse(text[3]), Int32.Parse(text[4]));
                }
                lineNumber++;
            }
            foreach (Grid grid in GridList)
            {
                if (grid.GridName != "WALL")
                {
                    Path path = new Path(grid);
                    PathList.Add(path);
                }
            }
        }

        public void AddGrids(int lengthY, int lengthX)
        {
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    Point pt = new Point(x, y);
                    Grid grid = new Grid(pt);
                    _gridList.Add(grid);
                    grid.GridName = "PATH";
                }
            }
        }

        public void AddWalls(int x, int y, int width, int height)
        {
            for (int i = x; i < width + x; i++)
            {
                for (int j = y; j < height + y; j++)
                {
                    Point pt = new Point(i, j);
                    Grid wall = new Grid(pt);
                    _wallList.Add(wall);
                    foreach (Grid grid in _gridList)
                    {
                        if (wall.X == grid.X && wall.Y == grid.Y)
                        {
                            grid.GridName = "WALL";
                        }

                    }
                }
            }
        }

        // Add goal
        public void AddGoal(int x, int y)
        {
            Point pt = new Point(x, y);
            Grid goal = new Grid(pt);
            foreach(Grid grid in _gridList)
            {
                if (goal.X == grid.X && goal.Y == grid.Y)
                {
                    grid.GridName = "GOAL";
                }
            }
        }
        
        // Add adjacent grids
        public void AddAdjacentGrid(Grid grid)
        {
            List<Path> temp = new List<Path>();
            foreach (Path p in PathList)
            {
                if (p.Location.X == grid.X && p.Location.Y == grid.Y - 1)
                {
                   // up
                    p.Location.Sequence = 1;
                    temp.Add(p);
                }
                else if (p.Location.X == grid.X - 1 && p.Location.Y == grid.Y)
                {
                   // left
                    p.Location.Sequence = 2;
                    temp.Add(p);
                }
                else if (p.Location.X == grid.X && p.Location.Y == grid.Y + 1)
                {
                   // down
                    p.Location.Sequence = 3;
                    temp.Add(p);
                }
                else if (p.Location.X == grid.X + 1 && p.Location.Y == grid.Y)
                {
                   // right
                    p.Location.Sequence = 4;
                    temp.Add(p);
                }
                
            }
            // Reorder the adjacent node in a given sequence (up -> left -> down -> right)
            List<Path> l = new List<Path>();
            l = temp.OrderBy(x => x.Location.Sequence).ToList();
            grid.AdjecentGrids = l;
        }

        public List<Grid> GridList { get => _gridList; set => _gridList = value; }
        public List<Grid> WallList { get => _wallList; set => _wallList = value; }
        public int RobotX { get => _robotX; set => _robotX = value; }
        public int RobotY { get => _robotY; set => _robotY = value; }
        internal List<Path> PathList { get => _pathList; set => _pathList = value; }
        public Resources Data { get => _data; set => _data = value; }
        public int MapLengthX { get => _mapLengthX; set => _mapLengthX = value; }
        public int MapLengthY { get => _mapLengthY; set => _mapLengthY = value; }
    }
}
