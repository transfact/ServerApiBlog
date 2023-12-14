using Microsoft.EntityFrameworkCore;

namespace ServerApiBlog.Models
{
    public class MemberBlogContext :DbContext
    {
        public MemberBlogContext(DbContextOptions<MemberBlogContext> options)
           : base(options)
        {
        }

        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Blog> Blogs { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Blogs)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust the deletion behavior if needed
        }
    }
}
