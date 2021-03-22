using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BattleShipApi
{
    public interface IBoard
    {
        string Id { get; set; }
        Point[,] Grid { get; set; }
        List<Ship> AllShips { get; set; }

        bool AddShip(Point start, Point end);
        bool Attack(Point point);
    }

    public class Board : IBoard
    {
        public string Id { get; set; }
        public Point[,] Grid { get; set; }


        public Board()
        {
            this.Grid = new Point[10, 10];
            this.AllShips = new List<Ship>();

        }

        public List<Ship> AllShips { get; set; }

        public bool AddShip(Point start, Point end)
        {
            if (IsPointOutSideGrid(start) || IsPointOutSideGrid(end))
                throw new Exception("Ship outside Board");

            foreach (Point occupiedPoint in Grid)
            {
                if (occupiedPoint != null && ((start.X == occupiedPoint.X && start.Y == occupiedPoint.Y) || (end.X == occupiedPoint.X && end.Y == occupiedPoint.Y))
                )
                    throw new Exception("Ship already present");
            }
            FillGridPoints(start, end);
            AllShips.Add(new Ship(start, end));
            return true;

        }

        public bool Attack(Point attackPoint)
        {
            if (Grid[attackPoint.X, attackPoint.Y] != null && Grid[attackPoint.X, attackPoint.Y].Status == PointStatus.Filled)
                return true;
            else
                return false;
        }

        private bool IsPointOutSideGrid(Point point)
        {
            return point.X > 9 || point.X < 0 || point.Y > 9 || point.Y < 0;
        }

        private void FillGridPoints(Point p1, Point p2)
        {
            if (p1.X == p2.X)
            {
                int minY, maxY;
                minY = Math.Min(p1.Y, p2.Y);
                maxY = Math.Max(p1.Y, p2.Y);

                for (int y = minY; y <= maxY; y++)
                {
                    Point p = new Point(p1.X, y, PointStatus.Filled);
                    Grid[p1.X, y] = p;
                }
            }
            else if (p1.Y == p2.Y)
            {
                int minX, maxX;
                minX = Math.Min(p1.X, p2.X);
                maxX = Math.Max(p1.X, p2.X);

                for (int x = minX; x <= maxX; x++)
                {
                    Point p = new Point(x, p1.Y, PointStatus.Filled);
                    Grid[x, p1.Y] = p;
                }
            }
            else //Diagnoal lines not allowed
                throw new Exception("Ship has to be placed vertically or horizontally");
        }

    }
}