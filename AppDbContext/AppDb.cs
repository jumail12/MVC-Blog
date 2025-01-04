using Microsoft.EntityFrameworkCore;
using MVC_BlogWebApp.Models.BlogModel;

namespace MVC_BlogWebApp.AppDbContext
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }   

        public DbSet<Blog> Blogs { get; set; }

    }
}
