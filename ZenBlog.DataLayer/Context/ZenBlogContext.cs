using Microsoft.EntityFrameworkCore;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.DataLayer.Context
{
    public class ZenBlogContext : DbContext
    {
        public ZenBlogContext(DbContextOptions<ZenBlogContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FullName = "Sadra Boromand",
                    Email = "sadrabroo@gmail.com",
                    phone = "09140286763",
                    Role = UserRole.admin,
                    Password = "123",
                    Image = "/Images/Profiles/Default.png"
                }
                );
            modelBuilder.Entity<Setting>().HasData(
                new Setting()
                {
                    Id = 1,
                    AboutUs = "متن برای درباره ما",
                    Address = "ایران- اصفهان",
                    Phones = "09140286763",
                    Email = "sadrabroo@gmail.com",
                    Image = ""
                }
                );
        }
    }
}
