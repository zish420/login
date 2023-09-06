using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace login.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ?Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string ?Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string ?Password { get; set; }
    }

    public class LoginDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity mappings or relationships here if needed
        }
    }


}
