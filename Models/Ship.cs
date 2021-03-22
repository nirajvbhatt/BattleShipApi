using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipApi
{
    public class Ship
    {

        Point StartPoint { get; set; }
        Point EndPoint { get; set; }

        public Ship(Point startPoint, Point endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }
    }
}