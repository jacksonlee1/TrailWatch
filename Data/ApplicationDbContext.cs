using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Data.Entities.Identity;

namespace Data;

public partial class ApplicationDbContext : IdentityDbContext<User, RoleEntity, int, UserClaimEntity, UserRoleEntity, UserLoginEntity, RoleClaimEntity, UserTokenEntity>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Trail> Trails { get; set; }

    public override DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TrailWatchDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC074884B470");

            entity.ToTable("Comment");

            entity.Property(e => e.Content).HasMaxLength(100);

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__PostId__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__UserId__4E88ABD4");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC070B713F7E");

            entity.ToTable("Post");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.TrailId).HasColumnName("Trail Id");

            entity.HasOne(d => d.Region).WithMany(p => p.Posts)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK__Post__RegionId__4AB81AF0");

            entity.HasOne(d => d.Trail).WithMany(p => p.Posts)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("FK__Post__Trail Id__49C3F6B7");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Region__3214EC07E8E3DE19");

            entity.ToTable("Region");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.Admin).WithMany(p => p.Regions)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__Region__AdminId__4316F928");
        });

        modelBuilder.Entity<Trail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trail__3214EC078840AE99");

            entity.ToTable("Trail");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.Admin).WithMany(p => p.Trails)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__Trail__AdminId__45F365D3");

            entity.HasOne(d => d.Region).WithMany(p => p.Trails)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK__Trail__RegionId__46E78A0C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07DF3888B1");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);

            modelBuilder.Entity<User>()
    .ToTable("Users")
    .Ignore(u => u.UserName); // Ignore duplicate column

            modelBuilder.Entity<RoleEntity>().ToTable("Roles");
            modelBuilder.Entity<UserRoleEntity>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaimEntity>().ToTable("UserClaims");
            modelBuilder.Entity<UserLoginEntity>().ToTable("UserLogins");
            modelBuilder.Entity<UserTokenEntity>().ToTable("UserTokens");
            modelBuilder.Entity<RoleClaimEntity>().ToTable("RoleClaims");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
