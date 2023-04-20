namespace DoleEcIntranet.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityIntranet : DbContext
    {
        public EntityIntranet()
            : base("name=EntityIntranet")
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Contenidos> Contenidos { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<GastosPersonales> GastosPersonales { get; set; }
        public virtual DbSet<General_Data> General_Data { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.GastosPersonales)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categorias>()
                .HasMany(e => e.Contenidos)
                .WithRequired(e => e.Categorias)
                .HasForeignKey(e => e.IdCategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estados>()
                .HasMany(e => e.Contenidos)
                .WithRequired(e => e.Estados)
                .HasForeignKey(e => e.IdEstado)
                .WillCascadeOnDelete(false);
        }

        
    }
}
