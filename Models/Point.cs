using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BattleShipApi.Model
{
    public enum PointStatus
    {
        [EnumMember(Value = "Filled")]
        Filled
    }

    public class Point
    {

        public int X { get; set; }
        public int Y { get; set; }

        [JsonIgnore]
        internal PointStatus Status { get; set; }

        public Point(int x, int y, PointStatus status)
        {
            this.X = x;
            this.Y = y;
            this.Status = status;
        }
    }
}