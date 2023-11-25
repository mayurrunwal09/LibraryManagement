using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions option) :  base(option)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Borrowed_Book> Borrowed_Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrowed_Book>()
                .HasOne<User>(u => u.User)
                .WithMany(b => b.Borrowed_Books)
                .HasForeignKey(f => f.UserID)
                .IsRequired();

            modelBuilder.Entity<Borrowed_Book>()
                .HasOne<Book>(u => u.Books)
                .WithMany(b => b.Borrowed_Books)
                .HasForeignKey(f => f.BookID)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne<UserType>(u => u.UserType)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.TypeId)
                .IsRequired();
        }
    }
}
