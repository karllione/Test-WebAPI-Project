using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
    public class TablesContext : DbContext
    {
        public TablesContext (DbContextOptions<TablesContext> options) : base(options) 
        {

        }  

        public DbSet<District> Districts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }  
        public DbSet<Specialization> Specializations { get; set; }
    }
}
