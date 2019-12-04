using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:dbsaee.database.windows.net,1433;Initial Catalog=BDSAEE;Persist Security Info=False;User ID=saee;Password=Proyecto123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
