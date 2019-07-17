using Blog.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Blog.DAL
{
    public class BlogContext : IdentityDbContext<ApplicationUser>
    {
        public BlogContext() : base("BlogContext")
        {

        }

        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public static BlogContext Create()
        {
            return new BlogContext();
        }

    }
}