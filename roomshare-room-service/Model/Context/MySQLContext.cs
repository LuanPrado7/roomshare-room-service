using Microsoft.EntityFrameworkCore;

namespace roomshare_room_service.Model.Context
{
    public class MySQLContext: DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { 
            this.Database.EnsureCreated();
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
