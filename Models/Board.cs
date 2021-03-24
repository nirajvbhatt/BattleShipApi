using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BattleShipApi.Model
{
    public interface IBoard
    {
        string Id { get; set; }
        Point[,] Grid { get; set; }
        List<Ship> AllShips { get; set; }

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


    }
}