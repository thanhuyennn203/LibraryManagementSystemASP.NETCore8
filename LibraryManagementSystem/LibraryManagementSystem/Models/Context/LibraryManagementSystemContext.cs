using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models.Context
{
    public class LibraryManagementSystemContext : DbContext
    {
        public LibraryManagementSystemContext(DbContextOptions<LibraryManagementSystemContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Carousel> Carousel { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Loan> Loan { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
           .HasOne(b => b.Category)
           .WithMany(c => c.Books)
           .HasForeignKey(b => b.CategoryId);
        }
    }
}
