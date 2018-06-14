using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConnectWeb.Models
{
    public partial class D3ConnectContext : DbContext
    {
        public D3ConnectContext()
        {
        }

        public D3ConnectContext(DbContextOptions<D3ConnectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var conn = Configuration.Get("Data:DefaultConnection:ConnectionString");
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=D3Connect;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
