using Microsoft.EntityFrameworkCore;
using MovieFlix.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Infrastructure
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasOne<Rental>(s => s.Rental)
                .WithMany(r => r.Members)
                .HasForeignKey(s => s.RentalId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.MovieRentals)
                .WithOne(mr => mr.Movie)
                .HasForeignKey(mr => mr.MovieId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Configure delete behavior

            modelBuilder.Entity<MovieRental>()
                .HasOne(mr => mr.Movie)
                .WithMany(m => m.MovieRentals)
                .HasForeignKey(mr => mr.MovieId);

            modelBuilder.Entity<Rental>()
                .Property(p => p.TotalCost)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MovieRental> MovieRentals { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
