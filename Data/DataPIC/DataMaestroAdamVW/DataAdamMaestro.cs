using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoleEcIntranet.Data.DataPIC.DataMaestroAdamVW
{
    public partial class DataAdamMaestro : DbContext
    {
        public DataAdamMaestro()
            : base("name=DataAdamMaestro")
        {
        }

        public virtual DbSet<vw_maestro_SharePoint> vw_maestro_SharePoint { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.compania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.nombre_cia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.trabajador)
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.nombre_trab)
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.depto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.depto_des)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.centr_costo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.centro_costo_des)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.area_resp)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.area_resp_des)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.puesto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.nombre_puesto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.div_pais)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.div_pais_des)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.e_mail)
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.codigo_jefe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.nombre_jefe)
                .IsUnicode(false);

            modelBuilder.Entity<vw_maestro_SharePoint>()
                .Property(e => e.cedula)
                .IsUnicode(false);
        }
    }
}
