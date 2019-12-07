using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SAEE_WEB.Models
{
    public partial class BDSAEEContext : DbContext
    {
        public BDSAEEContext()
        {
        }

        public BDSAEEContext(DbContextOptions<BDSAEEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudiantes> Estudiantes { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Profesores> Profesores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiantes>(entity =>
            {
                entity.ToTable("estudiantes");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrimerApellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SegundoApellido)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Pin)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Grupos>(entity =>
            {
                entity.ToTable("grupos");

                entity.Property(e => e.Grupo)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Profesores>(entity =>
            {
                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Contrasenia)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrimerApellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SegundoApellido)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
