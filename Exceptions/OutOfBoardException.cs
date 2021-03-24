
using System;

namespace BattleShipApi.Exceptions
{
    public class OutOfBoardException : Exception
    {
        public OutOfBoardException(string message) : base(message)
        { }
    }
}