using Microsoft.EntityFrameworkCore;
using LibraryManagementSystemTesting.Models; // Adjust if you have a different folder structure for models

namespace LibraryManagementSystemTesting.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<BooksAdmin> Books { get; set; }
    }
}
