using Microsoft.EntityFrameworkCore;
using System.Globalization;
namespace WebAPI.Models
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext()
        {

        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<reg> reg { get; set; }

    }
}
