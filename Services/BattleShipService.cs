using System;
using BattleShipApi.Model;
using BattleShipApi.Exceptions;
using Newtonsoft.Json;

namespace BattleShipApi.Services
{
    public class BattleShipService : IBattleShipService
    {
        private IBoard _battleShipBoard;
        public BattleShipService(IBoard battleShipBoard)
        {
            _battleShipBoard = battleShipBoard;
        }

        /// <summary>
        /// Returns 2-dimensional Point object array as json
        /// </summary>
        public string GetBoard()
        {
            return JsonConvert.SerializeObject(_battleShipBoard.Grid);
        }

        /// <summary>
        /// Adds a ship at specified start and end points (x,y) objects
        /// </summary>
        /// <param name="start">Point object specifying x and y coordiantes of starting position of ship - minimum (0,0)</param>
        /// <param name="point">Point object specifying x and y coordiante of ending position of ship - maximum (9,9)</param>
        /// <exception>AlreadyExistsException</exception>
        public void AddShip(Point start, Point end)
        {
            if (IsPointOutSideGrid(start) || IsPointOutSideGrid(end))
                throw new OutOfBoardException("Ship outside Board");

            foreach (Point occupiedPoint in _battleShipBoard.Grid)
            {
                if (occupiedPoint != null && ((start.X == occupiedPoint.X && start.Y == occupiedPoint.Y) || (end.X == occupiedPoint.X && end.Y == occupiedPoint.Y))
                )
                    throw new ShipAlreadyExistsException("Ship already present");
            }
            FillGridPoints(start, end);
            _battleShipBoard.AllShips.Add(new Ship(start, end));
        }

        /// <summary>
        /// Adds a ship at specified start and end points (x,y) objects/>s
        /// </summary>
        /// <exception>NotFoundException</exception>
        public bool Attack(Point attackPoint)
        {
            if (_battleShipBoard.Grid[attackPoint.X, attackPoint.Y] != null && _battleShipBoard.Grid[attackPoint.X, attackPoint.Y].Status == PointStatus.Filled)
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
                    _battleShipBoard.Grid[p1.X, y] = p;
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
                    _battleShipBoard.Grid[x, p1.Y] = p;
                }
            }
            else //Diagnoal lines not allowed
                throw new ShipNotAlignedException("Ship has to be placed vertically or horizontally");
        }


    }
}