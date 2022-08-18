﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure;

public class DbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options, ServiceLifetime serviceLifetime) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //BOOK MODEL BUILDER
        //Auto generate ID
        modelBuilder.Entity<Book>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        //Foregin key to author ID
        modelBuilder.Entity<Book>()
            .HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        //Don't auto include author on query
        modelBuilder.Entity<Book>()
            .Ignore(b => b.Author);
        
        //AUTHOR MODEL BUILDER
        //Auto generate ID
        modelBuilder.Entity<Author>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
<<<<<<< Updated upstream
        modelBuilder.Entity<Student>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

=======
        
        //AUTHOR MODEL BUILDER
        //Auto generate ID
        modelBuilder.Entity<Library>().Property(f => f.Id).ValueGeneratedOnAdd();
>>>>>>> Stashed changes
    }
    
    //Mapping to entity classes
    public DbSet<Author> Author { get; set; }
    public DbSet<Book> Book { get; set; }
<<<<<<< Updated upstream
    
    public DbSet<Student> Student { get; set; }
=======
    public DbSet<Library> Library { get; set;}
>>>>>>> Stashed changes
}