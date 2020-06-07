using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Howest_Movies_EE_DAL.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GenreMovie> GenreMovie { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<MovieRole> MovieRole { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreMovie>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreId })
                    .HasName("PK_genre_movie_raw");

                entity.ToTable("genre_movie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.GenreMovie)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_genre_movie_genres");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.GenreMovie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_genre_movie_movies");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImdbName)
                    .HasColumnName("imdb_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MovieRole>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.PersonId, e.Role });

                entity.ToTable("movie_role");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieRole)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_role_movies");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MovieRole)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_role_persons");
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoverUrl)
                    .IsRequired()
                    .HasColumnName("cover_url")
                    .HasMaxLength(500);

                entity.Property(e => e.ImdbId)
                    .IsRequired()
                    .HasColumnName("imdb_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Kind)
                    .IsRequired()
                    .HasColumnName("kind")
                    .HasMaxLength(50);

                entity.Property(e => e.OriginalAirDate)
                    .IsRequired()
                    .HasColumnName("original_air_date")
                    .HasMaxLength(50);

                entity.Property(e => e.Plot)
                    .HasColumnName("plot")
                    .HasColumnType("text");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100);

                entity.Property(e => e.Top250Rank).HasColumnName("top_250_rank");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.ToTable("persons");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Biography)
                    .IsRequired()
                    .HasColumnName("biography");

                entity.Property(e => e.ImdbId)
                    .IsRequired()
                    .HasColumnName("imdb_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
