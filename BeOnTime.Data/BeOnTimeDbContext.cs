namespace Be_on_time.Data
{
    using Microsoft.EntityFrameworkCore;
    using Be_on_time.Data.Models;

    public class BeOnTimeDbContext : DbContext
    {
        public BeOnTimeDbContext(DbContextOptions<BeOnTimeDbContext> options) : base()
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<UsersMeetings> UsersMeetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersMeetings>()
                .HasKey(um => new { um.UserId, um.MeetingId });
            modelBuilder.Entity<UsersMeetings>()
                .HasOne(um => um.User)
                .WithMany(u => u.UsersMeetings)
                .HasForeignKey(u => u.MeetingId);
            modelBuilder.Entity<UsersMeetings>()
                .HasOne(um => um.Meeting)
                .WithMany(m => m.UsersMeetings)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Meeting>()
                .HasMany(m => m.Feedbacks);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User);
        }
    }
}
