using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Troby.models
{
    public partial class trobyContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public trobyContext(DbContextOptions<trobyContext> options, IConfiguration config)
            : base(options)
        {
            _configuration = config;
        }

        public virtual DbSet<Achievements> Achievements { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Ownership> Ownership { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<Unlocks> Unlocks { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MySQLConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievements>(entity =>
            {
                entity.ToTable("achievements");

                entity.HasIndex(e => e.GameId)
                    .HasName("gameId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("tinytext");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(4)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Worth)
                    .HasColumnName("worth")
                    .HasColumnType("enum('bronze','silver','gold','platinum')");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("achievements_ibfk_1");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.ToTable("games");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Bgg)
                    .HasColumnName("bgg")
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("int(4)");
            });

            modelBuilder.Entity<Ownership>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("ownership");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_ownership_users_idx");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Ownership)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ownership_games");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ownership)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ownership_users");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.HasKey(e => e.TokenString)
                    .HasName("PRIMARY");

                entity.ToTable("tokens");

                entity.HasIndex(e => e.Owner)
                    .HasName("FK_tokens_users_idx");

                entity.Property(e => e.TokenString)
                    .HasColumnName("tokenString")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("dateCreated")
                    .HasColumnType("date");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tokens_users");
            });

            modelBuilder.Entity<Unlocks>(entity =>
            {
                entity.ToTable("unlocks");

                entity.HasIndex(e => e.AchId)
                    .HasName("FK_unlocks_achievements");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_unlocks_users_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AchId)
                    .HasColumnName("achId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Unlockscol)
                    .HasColumnName("unlockscol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Ach)
                    .WithMany(p => p.Unlocks)
                    .HasForeignKey(d => d.AchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_unlocks_achievements");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Unlocks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_unlocks_users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Hash)
                    .HasColumnName("hash")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasColumnType("varchar(255)");
            });
        }
    }
}
