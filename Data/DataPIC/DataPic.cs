using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoleEcIntranet.Data.DataPIC
{
    public partial class DataPic : DbContext
    {
        public DataPic()
            : base("name=DataPic")
        {
        }

        public virtual DbSet<AgenciaViajes> AgenciaViajes { get; set; }
        public virtual DbSet<AgenciaViajes_Det> AgenciaViajes_Det { get; set; }
        public virtual DbSet<Cab_Trans> Cab_Trans { get; set; }
        public virtual DbSet<Cab_transFae> Cab_transFae { get; set; }
        public virtual DbSet<CentroCostos2> CentroCostos2 { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Det_Trans> Det_Trans { get; set; }
        public virtual DbSet<Det_TransFae> Det_TransFae { get; set; }
        public virtual DbSet<Det_TransFaeAlcance> Det_TransFaeAlcance { get; set; }
        public virtual DbSet<DirectorAreas> DirectorAreas { get; set; }
        public virtual DbSet<Estados_Flujos> Estados_Flujos { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<Fae_Asistentes> Fae_Asistentes { get; set; }
        public virtual DbSet<Flujo> Flujo { get; set; }
        public virtual DbSet<GerentesAreas> GerentesAreas { get; set; }
        public virtual DbSet<Invitados> Invitados { get; set; }
        public virtual DbSet<Maestro_SharePoint> Maestro_SharePoint { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Sharepoint_File> Sharepoint_File { get; set; }
        public virtual DbSet<Sharepoint_Flujos> Sharepoint_Flujos { get; set; }
        public virtual DbSet<Sharepoint_Monedas> Sharepoint_Monedas { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TC_EstadoCuenta> TC_EstadoCuenta { get; set; }
        public virtual DbSet<TC_Files> TC_Files { get; set; }
        public virtual DbSet<TC_Invitados> TC_Invitados { get; set; }
        public virtual DbSet<TC_Justificacion> TC_Justificacion { get; set; }
        public virtual DbSet<TipoConsumo> TipoConsumo { get; set; }
        public virtual DbSet<TipoMoneda> TipoMoneda { get; set; }
        public virtual DbSet<TipoReportes> TipoReportes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cab_Trans>()
                .Property(e => e.SlDestino)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Cab_Trans>()
                .Property(e => e.SlTipoConsumo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Cab_Trans>()
                .Property(e => e.Estado)
                .IsFixedLength();

            modelBuilder.Entity<Det_Trans>()
                .Property(e => e.Tcambio)
                .HasPrecision(12, 4);

            modelBuilder.Entity<Det_Trans>()
                .Property(e => e.Monto)
                .HasPrecision(12, 4);

            modelBuilder.Entity<Det_Trans>()
                .Property(e => e.Valor)
                .HasPrecision(12, 4);

            modelBuilder.Entity<Det_TransFae>()
                .Property(e => e.valor)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Det_TransFae>()
                .Property(e => e.total)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Det_TransFaeAlcance>()
                .Property(e => e.valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Det_TransFaeAlcance>()
                .Property(e => e.total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Facturas>()
                .Property(e => e.TipoFactura)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Flujo>()
                .Property(e => e.Estado)
                .IsFixedLength();

            modelBuilder.Entity<Paises>()
                .HasMany(e => e.Ciudades)
                .WithRequired(e => e.Paises)
                .HasForeignKey(e => e.IdPais)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sharepoint_Flujos>()
                .Property(e => e.Estado)
                .IsFixedLength();
        }
    }
}
