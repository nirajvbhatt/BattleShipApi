using Microsoft.EntityFrameworkCore;

namespace BattleShipApi
{
    public class BoardContext : DbContext
    {
        public BoardContext(DbContextOptions<BoardContext> options)
            : base(options)
        { }
        public DbSet<Board> BattleShipGameBoard { get; set; }
        public DbSet<Ship> Ships { get; set; }

    }
}