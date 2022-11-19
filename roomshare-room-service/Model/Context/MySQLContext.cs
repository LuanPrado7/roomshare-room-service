using Microsoft.EntityFrameworkCore;

namespace roomshare_room_service.Model.Context
{
    public class MySQLContext: DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
    }
}
