using Microsoft.EntityFrameworkCore;

namespace TimesheetSystem.Models
{
    public class TimesheetDbContext : DbContext
    {
        public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options) { }

        public DbSet<TimesheetEntry> TimesheetEntries { get; set; }

        //Ensuring EntityFramework is aware of the in-memory storage


    }
}
