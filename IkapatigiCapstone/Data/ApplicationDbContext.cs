using System;
using System.Collections.Generic;
using IkapatigiCapstone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<Forum> Forums { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostReply> PostReplies { get; set; }
        public virtual DbSet<AddRequestDiagnostic> AddRequestDiagnostics { get; set; } = null!;
        public virtual DbSet<Cure> Cures { get; set; } = null!;
        public virtual DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public virtual DbSet<HowTo> HowTos { get; set; } = null!;
        public virtual DbSet<PlantDisease> PlantDiseases { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DESKTOP-KJFVQAM\\MSSQLSERVER2;Database=FloraDB;UID=sa;PWD=benilde;MultipleActiveResultSets=true;");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnection"));
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
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserID");

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

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
