﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Entities;
using TatBlog.Data.Mappings;

namespace TatBlog.Data.Contexts;

public class BlogDbContext : DbContext
{
    public DbSet<Author> Authors {get;set;}

    public DbSet<Category> Categories  {get;set;}

    public DbSet<Post> Posts  {get;set;}

    public DbSet<Tag> Tags {get;set;}
    
  /*  protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-1U9L5GF;Database=TagBlog;
            Trusted_Connection=True;MultipleActiveResultSets=True;
            TrustServerCertificate=true");
    }*/
    public BlogDbContext(DbContextOptions<BlogDbContext> options) 
        : base(options)
    {

    }

    public BlogDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CategoryMap).Assembly);
    }

}
