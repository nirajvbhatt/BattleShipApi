
using BattleShipApi.Model;

namespace BattleShipApi.Services
{
    public interface IBattleShipService
    {
        /// <summary>
        /// Returns 2-dimensional Point object array as json
        /// </summary>
        string GetBoard();

        /// <summary>
        /// Adds a ship at specified start and end points (x,y) objects
        /// </summary>
        /// <param name="start">Point object specifying x and y coordiantes of starting position of ship - minimum (0,0)</param>
        /// <param name="point">Point object specifying x and y coordiante of ending position of ship - maximum (9,9)</param>
        /// <exception cref="NotFoundException"></exception>
        void AddShip(Point start, Point end);

        /// <summary>
        /// Attacks BattleShip board at given point (x,y)
        /// </summary>
        /// <param name="point">Point object specifying x and y coordiante</param>
        /// <returns>True if a ship is hit, False otherwise</returns>
        bool Attack(Point point);
    }
}