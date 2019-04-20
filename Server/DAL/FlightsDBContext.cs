using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SharedLibrary;

namespace Server.DAL
{
    class FlightsDBContext:DbContext
    {
        public DbSet<Flight> Flight { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ForSqlServerUseIdentityColumns();
            builder.Entity<Flight>().HasKey(flight => flight.ID);
            builder.Entity<Flight>().Property(flight=>flight.ID).ValueGeneratedOnAdd();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBConnection.Instance.ConnectionString);
        }
    }
}
