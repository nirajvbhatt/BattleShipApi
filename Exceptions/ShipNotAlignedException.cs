
using System;

namespace BattleShipApi.Exceptions
{
    public class ShipNotAlignedException : Exception
    {
        public ShipNotAlignedException(string message) : base(message)
        { }
    }
}