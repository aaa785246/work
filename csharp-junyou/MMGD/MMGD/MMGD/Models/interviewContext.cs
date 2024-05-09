﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MMGD.Models;

public partial class interviewContext : DbContext
{
    public interviewContext(DbContextOptions<interviewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<article> article { get; set; }

    public virtual DbSet<collect_article> collect_article { get; set; }

    public virtual DbSet<interaction_article> interaction_article { get; set; }

    public virtual DbSet<like_article> like_article { get; set; }

    public virtual DbSet<message_floor> message_floor { get; set; }

    public virtual DbSet<notice> notice { get; set; }

    public virtual DbSet<userData> userData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<article>(entity =>
        {
            entity.Property(e => e.article_number).ValueGeneratedNever();
        });

        modelBuilder.Entity<interaction_article>(entity =>
        {
            entity.Property(e => e.article_number).ValueGeneratedNever();
        });

        modelBuilder.Entity<userData>(entity =>
        {
            entity.HasKey(e => new { e.email, e.username }).HasName("PK_userData_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}