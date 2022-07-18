﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlyFundsAPI.BusinessObjects;

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(OnlyFundsDBContext))]
    [Migration("20220718002958_AddAvatarURL")]
    partial class AddAvatarURL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Bookmark", b =>
                {
                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("PostID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Comment", b =>
                {
                    b.Property<int?>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CommentTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.Property<int?>("UploaderID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("PostID");

                    b.HasIndex("UploaderID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.CommentLike", b =>
                {
                    b.Property<int>("CommentID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CommentID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("CommentLikes");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Donation", b =>
                {
                    b.Property<int>("DonationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<DateTime>("DonationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DonatorID")
                        .HasColumnType("int");

                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.HasKey("DonationID");

                    b.HasIndex("DonatorID");

                    b.HasIndex("PostID");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Follow", b =>
                {
                    b.Property<int>("FolloweeID")
                        .HasColumnType("int");

                    b.Property<int>("FollowerID")
                        .HasColumnType("int");

                    b.HasKey("FolloweeID", "FollowerID");

                    b.HasIndex("FollowerID");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NotificationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiverID")
                        .HasColumnType("int");

                    b.HasKey("NotificationID");

                    b.HasIndex("ReceiverID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.OTP", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserID", "Code");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("OTPs");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("AttachmentType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("FileURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Preview")
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UploaderID")
                        .HasColumnType("int");

                    b.HasKey("PostID");

                    b.HasIndex("UploaderID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.PostLike", b =>
                {
                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PostID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.PostTag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TagID");

                    b.HasIndex("TagName")
                        .IsUnique();

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.PostTagMap", b =>
                {
                    b.Property<int>("TagID")
                        .HasColumnType("int");

                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.Property<int?>("PostID1")
                        .HasColumnType("int");

                    b.HasKey("TagID", "PostID");

                    b.HasIndex("PostID");

                    b.HasIndex("PostID1");

                    b.ToTable("PostTagMaps");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Report", b =>
                {
                    b.Property<int>("ReporterID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("ReportID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReportTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReportType")
                        .HasColumnType("int");

                    b.Property<int>("ReportedObjectID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ReporterID");

                    b.HasIndex("ReporterID", "ReportedObjectID", "ReportType")
                        .IsUnique();

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("Banned")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("char(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Bookmark", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.Post", "Post")
                        .WithMany("Bookmarks")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Comment", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Uploader")
                        .WithMany("Comments")
                        .HasForeignKey("UploaderID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.CommentLike", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Donation", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Donator")
                        .WithMany()
                        .HasForeignKey("DonatorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Donator");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Follow", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Followee")
                        .WithMany("Follows")
                        .HasForeignKey("FolloweeID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FollowerID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Followee");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Notification", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.OTP", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "User")
                        .WithOne("OTP")
                        .HasForeignKey("OnlyFundsAPI.BusinessObjects.OTP", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Post", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Uploader")
                        .WithMany("Posts")
                        .HasForeignKey("UploaderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.PostLike", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.PostTagMap", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlyFundsAPI.BusinessObjects.Post", null)
                        .WithMany("TagMaps")
                        .HasForeignKey("PostID1");

                    b.HasOne("OnlyFundsAPI.BusinessObjects.PostTag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Report", b =>
                {
                    b.HasOne("OnlyFundsAPI.BusinessObjects.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Comment", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.Post", b =>
                {
                    b.Navigation("Bookmarks");

                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("TagMaps");
                });

            modelBuilder.Entity("OnlyFundsAPI.BusinessObjects.User", b =>
                {
                    b.Navigation("Bookmarks");

                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Follows");

                    b.Navigation("OTP");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
