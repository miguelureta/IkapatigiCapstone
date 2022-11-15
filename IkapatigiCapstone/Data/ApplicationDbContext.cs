using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using IkapatigiCapstone.Models;

namespace IkapatigiCapstone.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddRequestDiagnostic> AddRequestDiagnostics { get; set; } = null!;
        public virtual DbSet<Cure> Cures { get; set; } = null!;
        public virtual DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public virtual DbSet<Forum> Forums { get; set; } = null!;
        public virtual DbSet<HowTo> HowTos { get; set; } = null!;
        public virtual DbSet<PlantDisease> PlantDiseases { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostReply> PostReplies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnection"));
                
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddRequestDiagnostic>(entity =>
            {
                entity.Property(e => e.AddRequestDiagnosticId).HasColumnName("AddRequestDiagnosticID");

                entity.Property(e => e.ApprovedUserId).HasColumnName("ApprovedUserID");

                entity.Property(e => e.CureName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DiseaseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImageName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RequestedUserId).HasColumnName("RequestedUserID");

                entity.Property(e => e.Srp)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("SRP");

                entity.Property(e => e.TagName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.RequestedUser)
                    .WithMany(p => p.AddRequestDiagnostics)
                    .HasForeignKey(d => d.RequestedUserId)
                    .HasConstraintName("FK_AddRequestDiagnostics_Users");
            });

            modelBuilder.Entity<Cure>(entity =>
            {
                entity.Property(e => e.CureId).HasColumnName("CureID");

                entity.Property(e => e.CureName)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Srp)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("SRP");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Diagnostic>(entity =>
            {
                entity.HasKey(e => e.DiagnosticsId)
                    .HasName("PK_Diagnostics_1");

                entity.Property(e => e.DiagnosticsId).HasColumnName("DiagnosticsID");

                entity.Property(e => e.CureId).HasColumnName("CureID");

                entity.Property(e => e.PictureCollectionFromId).HasColumnName("PictureCollectionFromID");

                entity.Property(e => e.PlantDiseaseId).HasColumnName("PlantDiseaseID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Cure)
                    .WithMany(p => p.Diagnostics)
                    .HasForeignKey(d => d.CureId)
                    .HasConstraintName("FK_Diagnostics_Cures");

                entity.HasOne(d => d.PlantDisease)
                    .WithMany(p => p.Diagnostics)
                    .HasForeignKey(d => d.PlantDiseaseId)
                    .HasConstraintName("FK_Diagnostics_PlantDiseases");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Diagnostics)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diagnostics_Status");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Diagnostics)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_Diagnostics_Tags");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.ToTable("Forum");
                entity.HasKey(e => e.ForumId);
                entity.Property(e => e.ForumId)
                    .HasColumnName("ForumID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Forums)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Forum_Users");
            });

            modelBuilder.Entity<HowTo>(entity =>
            {
                entity.HasKey(e => e.HowTosID);

                entity.Property(e => e.HowTosID).HasColumnName("HowTosID");

                entity.Property(e => e.ArticleBody)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PictureCollectionFromID).HasColumnName("PictureCollectionFromID");

                entity.Property(e => e.StatusID).HasColumnName("StatusID");

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.HowTos)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_HowTos_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HowTos)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_HowTos_Users");
            });

            modelBuilder.Entity<PlantDisease>(entity =>
            {
                entity.Property(e => e.PlantDiseaseId).HasColumnName("PlantDiseaseID");

                entity.Property(e => e.CureId).HasColumnName("CureID");

                entity.Property(e => e.DiseaseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PlantDiseases)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_PlantDiseases_Tags");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PlantDiseases)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PlantDiseases_Users");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.HasKey(e => e.PostId);

                entity.Property(e => e.PostId)
                    .HasColumnName("PostID");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.ForumId).HasColumnName("ForumID");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Forum)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ForumId)
                    .HasConstraintName("FK_Post_Forum");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_Users");
            });

            modelBuilder.Entity<PostReply>(entity =>
            {
                entity.ToTable("PostReply");

                entity.HasKey(e => e.PostReplyId);

                entity.Property(e => e.PostReplyId)
                    .HasColumnName("PostReplyID");

                entity.Property(e => e.Content).HasMaxLength(150);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReplies)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_PostReply_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReplies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PostReply_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Role1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusType)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tags_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordResetToken)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ResetTokenExpires).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Roles1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
