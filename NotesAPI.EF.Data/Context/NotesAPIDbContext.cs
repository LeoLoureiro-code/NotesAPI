using Microsoft.EntityFrameworkCore;
using NotesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPI.EF.Data.Context
{
    public class NotesAPIDbContext: DbContext
    {
        public NotesAPIDbContext(DbContextOptions<NotesAPIDbContext> options)
       : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(NotesAPIDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
