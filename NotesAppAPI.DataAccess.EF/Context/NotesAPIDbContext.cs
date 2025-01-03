using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NotesAppAPI.DataAccess.EF.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace NotesAppAPI.DataAccess.EF.Context;

public partial class NotesAPIDbContext : DbContext
{
    public NotesAPIDbContext()
    {
    }

    public NotesAPIDbContext(DbContextOptions<NotesAPIDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<NotesTag> NotesTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=50.6.160.84;database=arkedste_Notes;, ServerVersion.Parse("5.7.23-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_unicode_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.NoteId).HasColumnType("int(11)");
            entity.Property(e => e.NoteContent).HasColumnType("text");
            entity.Property(e => e.NoteTitle).HasMaxLength(45);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Notes_ibfk_1");
        });

        modelBuilder.Entity<NotesTag>(entity =>
        {
            entity.HasKey(e => new { e.NotesNoteId, e.NotesUserId, e.TagsTagId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("Notes_Tags");

            entity.HasIndex(e => e.NotesUserId, "Notes_UserId");

            entity.HasIndex(e => e.TagsTagId, "Tags_TagId");

            entity.Property(e => e.NotesNoteId)
                .HasColumnType("int(11)")
                .HasColumnName("Notes_NoteId");
            entity.Property(e => e.NotesUserId)
                .HasColumnType("int(11)")
                .HasColumnName("Notes_UserId");
            entity.Property(e => e.TagsTagId)
                .HasColumnType("int(11)")
                .HasColumnName("Tags_TagId");

            entity.HasOne(d => d.NotesNote).WithMany(p => p.NotesTags)
                .HasForeignKey(d => d.NotesNoteId)
                .HasConstraintName("Notes_Tags_ibfk_1");

            entity.HasOne(d => d.NotesUser).WithMany(p => p.NotesTags)
                .HasForeignKey(d => d.NotesUserId)
                .HasConstraintName("Notes_Tags_ibfk_2");

            entity.HasOne(d => d.TagsTag).WithMany(p => p.NotesTags)
                .HasForeignKey(d => d.TagsTagId)
                .HasConstraintName("Notes_Tags_ibfk_3");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PRIMARY");

            entity.HasIndex(e => e.TagName, "TagName").IsUnique();

            entity.Property(e => e.TagId).HasColumnType("int(11)");
            entity.Property(e => e.TagName).HasMaxLength(45);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.HasIndex(e => e.UserEmail, "UserEmail").IsUnique();

            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.UserEmail).HasMaxLength(45);
            entity.Property(e => e.UserPassword).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
