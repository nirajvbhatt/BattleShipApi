
using System;

namespace BattleShipApi.Exceptions
{
    public class ShipAlreadyExistsException : Exception
    {
        public ShipAlreadyExistsException(string message) : base(message)
        { }
    }
}