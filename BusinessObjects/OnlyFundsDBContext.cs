using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OnlyFundsAPI.BusinessObjects
{
    public class OnlyFundsDBContext : DbContext
    {
        public OnlyFundsDBContext() : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostCategoryMap> PostCategoryMaps { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OTP> OTPs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(".").ToString() + Path.DirectorySeparatorChar + "BusinessObjects")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("OnlyFundsDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.UserID);
                entity.Property(user => user.Email)
                    .HasMaxLength(255)
                    .IsRequired();
                entity.HasIndex(user => user.Email)
                    .IsUnique();
                entity.Property(user => user.Username)
                    .HasMaxLength(32)
                    .IsRequired();
                entity.HasIndex(user => user.Username)
                    .IsUnique();
                entity.Property(user => user.Password)
                    .HasMaxLength(64)
                    .HasColumnType("char(64)")
                    .IsRequired();
                entity.Property(user => user.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(user => user.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(user => user.Banned)
                    .HasDefaultValue(false);
                entity.Property(user => user.Active)
                    .HasDefaultValue(true);
            });

            builder.Entity<Post>(entity =>
            {
                entity.HasKey(post => post.PostID);
                entity.HasOne(post => post.Uploader)
                    .WithMany(user => user.Posts)
                    .HasForeignKey(post => post.UploaderID)
                    .IsRequired();
                entity.Property(post => post.Title)
                    .HasMaxLength(300)
                    .IsRequired();
                entity.Property(post => post.Description)
                    .HasMaxLength(3000);
                entity.Property(post => post.Status)
                    .HasDefaultValue(PostStatus.Active);
                entity.Property(post => post.UploadTime)
                    .IsRequired();
                entity.Property(post => post.Preview)
                    .HasMaxLength(1500);
            });

            builder.Entity<Comment>(entity =>
            {
                entity.HasKey(cmt => cmt.CommentID);
                entity.Property(cmt => cmt.Content)
                    .HasMaxLength(500)
                    .IsRequired();
                entity.Property(cmt => cmt.IsActive)
                    .HasDefaultValue(true);
                entity.HasOne(cmt => cmt.Uploader)
                    .WithMany(user => user.Comments)
                    .HasForeignKey(cmt => cmt.UploaderID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
                entity.HasOne(cmt => cmt.Post)
                    .WithMany(post => post.Comments)
                    .HasForeignKey(cmt => cmt.PostID)
                    .IsRequired();
                entity.Property(cmt => cmt.CommentTime)
                    .IsRequired();
            });

            builder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(category => category.CategoryID);
                entity.Property(category => category.CategoryName)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.HasIndex(category => category.CategoryName)
                    .IsUnique();
                entity.Property(category => category.Active)
                    .HasDefaultValue(true);
            });

            builder.Entity<PostCategoryMap>(entity =>
            {
                entity.HasKey(pc => new { pc.CategoryID, pc.PostID });
                entity.HasOne(pc => pc.Category)
                    .WithMany()
                    .HasForeignKey(pc => pc.CategoryID);
                entity.HasOne(pc => pc.Post)
                    .WithMany()
                    .HasForeignKey(pc => pc.PostID);
            });

            builder.Entity<PostLike>(entity =>
            {
                entity.HasKey(like => new { like.PostID, like.UserID });
                entity.HasOne(like => like.Post)
                    .WithMany(post => post.Likes)
                    .HasForeignKey(like => like.PostID);
                entity.HasOne(like => like.User)
                    .WithMany()
                    .HasForeignKey(like => like.UserID)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<CommentLike>(entity =>
            {
                entity.HasKey(like => new { like.CommentID, like.UserID });
                entity.HasOne(like => like.Comment)
                    .WithMany(post => post.Likes)
                    .HasForeignKey(like => like.CommentID);
                entity.HasOne(like => like.User)
                    .WithMany()
                    .HasForeignKey(like => like.UserID)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Donation>(entity =>
            {
                entity.HasKey(donation => donation.DonationID);
                entity.Property(donation => donation.Amount)
                    .HasColumnType("money")
                    .IsRequired();
                entity.Property(donation => donation.DonationTime)
                    .IsRequired();
                entity.HasOne(donation => donation.Post)
                    .WithMany()
                    .HasForeignKey(donation => donation.PostID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
                entity.HasOne(donation => donation.Donator)
                    .WithMany()
                    .HasForeignKey(donation => donation.DonatorID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            builder.Entity<Follow>(entity =>
            {
                entity.HasKey(follow => new { follow.FolloweeID, follow.FollowerID });
                entity.HasOne(follow => follow.Follower)
                    .WithMany(user => user.Followers)
                    .HasForeignKey(follow => follow.FollowerID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
                entity.HasOne(follow => follow.Followee)
                    .WithMany(user => user.Follows)
                    .HasForeignKey(follow => follow.FolloweeID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            builder.Entity<Bookmark>(entity =>
            {
                entity.HasKey(b => new { b.PostID, b.UserID });
                entity.HasOne(b => b.User)
                    .WithMany(user => user.Bookmarks)
                    .HasForeignKey(b => b.UserID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
                entity.HasOne(b => b.Post)
                    .WithMany(post => post.Bookmarks)
                    .HasForeignKey(b => b.PostID)
                    .IsRequired();
                entity.Property(b => b.Description)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            builder.Entity<Notification>(entity =>
            {
                entity.HasKey(noti => noti.NotificationID);
                entity.HasOne(noti => noti.Receiver)
                    .WithMany()
                    .HasForeignKey(noti => noti.ReceiverID)
                    .IsRequired();
                entity.Property(noti => noti.Content)
                    .HasMaxLength(3000)
                    .IsRequired();
                entity.Property(noti => noti.IsRead)
                    .HasDefaultValue(false);
                entity.Property(noti => noti.NotificationTime)
                    .IsRequired();
            });

            builder.Entity<Report>(entity =>
            {
                entity.HasKey(report => report.ReporterID);
                entity.HasIndex(report => new { report.ReporterID, report.ReportedObjectID, report.ReportType })
                    .IsUnique();
                entity.Property(report => report.ReportedObjectID)
                    .IsRequired();
                entity.Property(report => report.ReportType)
                    .IsRequired();
                entity.HasOne(report => report.Reporter)
                    .WithMany()
                    .HasForeignKey(report => report.ReporterID)
                    .IsRequired();
                entity.Property(report => report.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(report => report.Status)
                    .HasDefaultValue(ReportStatus.Unresolved)
                    .IsRequired();
                entity.Property(report => report.ReportTime)
                    .IsRequired();
            });

            builder.Entity<OTP>(entity =>
            {
                entity.HasKey(otp => new { otp.UserID, otp.Code });
                entity.HasOne(otp => otp.User)
                    .WithOne(user => user.OTP)
                    .IsRequired();
            });
        }
    }
}