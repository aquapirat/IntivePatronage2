using Microsoft.EntityFrameworkCore;

namespace IntivePatronage2.Model
{
    public class FizzBuzzContext : DbContext
    {
        public FizzBuzzContext(DbContextOptions<FizzBuzzContext> options) :
            base(options)
        {
        }

        public DbSet<FizzBuzzItem> FizzBuzzItems { get; set; }
    }
}
