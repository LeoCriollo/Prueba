using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoleEcIntranet.Models
{
    public class AppViewModels
    {

    }


    public class NoticiasDoleIndexViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe Ingresar un Título")]
        [DisplayName("Título"), StringLength(80, ErrorMessage = "Campo mínimo 1 máximo 80", MinimumLength = 1)]
        public string TituloArticulo { get; set; }
        public int Estado { get; set; }
        public int Categoria { get; set; }
        public long Order { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreador { get; set; }
        [Required(ErrorMessage = "Debe Ingresar un Resumen del Artículo")]
        [DisplayName("Resumen"), StringLength(200, ErrorMessage = "Campo mínimo 1 máximo 200", MinimumLength = 1)]
        public string ResumenContenido { get; set; }
        [Required(ErrorMessage = "Debe Ingresar un Artículo")]
        [DisplayName("Artículo")]
        [AllowHtml]
        public string Contenido { get; set; }

        [Required(ErrorMessage = "Debe cargar la Imagen")]
        [DisplayName("Imagen")]
        public HttpPostedFileBase ImageFile { get; set; }      


    }

    [Validator(typeof(DateRangeValidator))]
    public class ReclutamientoInternoViewModel
    {        
        public int Id { get; set; }        
              
    //    [Required(ErrorMessage = "Debe Ingresar un Artículo")]
        [DisplayName("Artículo")]
        [AllowHtml]
        public string Contenido { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessage = "Debe ingresar una Fecha de Publicación")]
        [DisplayName("Fecha de Publicación")]        
        public DateTime fechaPublicacion { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessage = "Debe Ingresar una Fecha de Expiración ")]
        [DisplayName("Fecha de Expiración")]
        //[DataType(DataType.DateTime)]
        public DateTime fechaExpiracion { get; set; }
    }


    public class DateRangeValidator : AbstractValidator<ReclutamientoInternoViewModel>
    {
        public DateRangeValidator()
        {
            RuleFor(x => x.fechaExpiracion).GreaterThan(x => x.fechaPublicacion ).WithMessage("Fecha debe ser menor.");
            RuleFor(x => x.Contenido).NotEmpty().WithMessage("Debe Ingresar un Artículo.");
        }
    }


    [Serializable]
    public class ArticuloViewModel
    {
        public int idArticulo { get; set; }
        public string titulo { get; set; }
        [AllowHtml]
        public string contenidoResumen { get; set; }
        [AllowHtml]
        public string contenido { get; set; }
        public string fechaPublicacion { get; set; }

        public int dia { get; set; }
        public string mes { get; set; }


    }

    /// <summary>
    /// Modelo de datos para users-rol
    /// </summary>
    public class UsersRoleViewModel{

        [Required]
        [DisplayName("Usuario")]
        public int IdUser { get; set; }
        [Required]
        [DisplayName("Rol")]
        public int IdRol { get; set; }


    }

    public class UsersRoleDetViewModel
    {
      
        
        public int IdUser { get; set; }
        
        
        public int IdRol { get; set; }
        [DisplayName("Usuario")]
        public string UserName { get; set; }
        [DisplayName("Rol")]
        public string RolName { get; set; }


    }

    public class RequestConsultaSlipPago
    {
        [Required]
        public int year { get; set; }
        [Required]
        public int periodo { get; set; }

    }

    public class RequestConsultaSlipPago2
    {
        [Required]
        public int year { get; set; }
        [Required]
        public int periodo { get; set; }
        [Required]
        public int codEmpleado { get; set; }
    }


    public class SlipPago
    {
        public SliPagoCab cabecera { get; set; }
        public List<SlipPagoDet> detalles { get; set; }
        [DisplayName("TOTAL HABERES")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal totalHaberes { get; set; }
        [DisplayName("TOTAL DESCUENTOS")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal totalDescuentos { get; set; }
        [DisplayName("NETO A PAGAR")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal netoAPagar { get; set; }


    }

    public class SliPagoCab
    {
        [DisplayName("Compañía")]
        public string compania { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Cédula")]
        public string cedula { get; set; }
        [DisplayName("Mes")]
        public string mes { get; set; }
        [DisplayName("Centro Costo")]
        public string centroCosto { get; set; }
        [DisplayName("Fecha de Pago")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime fechaPago { get; set; }
        [DisplayName("Código de Empleado")]
        public string codEmpleado { get; set; }
        [DisplayName("Cuenta")]
        public string cuenta { get; set; }
        [DisplayName("Mes")]
        public string mesP { get; set; }
        [DisplayName("Cargo del Empleado")]
        public string cargo { get; set; }
    }

    public class SlipPagoDet
    {
        public int tipoMov { get; set; }
        [DisplayName("Concepto")]
        public int idConcepto { get; set; }
        [DisplayName("Descripción")]
        public string descConcepto { get; set; }
        [DisplayName("Tiempo")]
        public decimal? tiempo { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DisplayName("Importe")]
        public decimal importe { get; set; }

    }

    public class RequestConsultaImptoRta
    {
        [Required]
        public int year { get; set; }
        [Required]
        public int periodo { get; set; }
    }


    public class ImptoRta
    {
        public ImptoRtaCab cab { get; set; }

        public List<ImptoRtaDet> det { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal totalSueldos { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal totalIngresosAnuales { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal aporteIess { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal baseImponible { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal diferencia { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal totalRetFecha { get; set; }

    }

    public class ImptoRtaCab
    {
        [DisplayName("Compañía")]
        public string compania { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Cédula")]
        public string cedula { get; set; }
        [DisplayName("Mes")]
        public string mes { get; set; }
        [DisplayName("Centro Costo")]
        public string centroCosto { get; set; }
        [DisplayName("Fecha de Pago")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaPago { get; set; }
        [DisplayName("Código de Empleado")]
        public string codEmpleado { get; set; }
        [DisplayName("Cuenta")]
        public string cuenta { get; set; }
        [DisplayName("Mes")]
        public string mesP { get; set; }
        [DisplayName("Cargo del Empleado")]
        public string cargo { get; set; }

       
    }

    public class ImptoRtaDet
    {

        public int idConcepto  { get; set; }
        public string descConcepto { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal valor { get; set; }

        public int tipoGrupo { get; set; }


    }

    public class CiudadHoteles
    {
        [DisplayName("Ciudad")]
        public string ciudad { get; set; }

        [DisplayName("Hoteles")]
        public string hoteles { get; set; }
        [DisplayName("Habitación Sencilla")]
        public string habitacionSencilla { get; set; }
        [DisplayName("E-mail")]
        public string email { get; set; }
        [DisplayName("Teléfono")]
        public string telefono { get; set; }
        
        public string codHotel { get; set; }
    }
    
    public class HotelConsult
    {
        public string codigo { get; set; }
    }


    public class HotelDetalle
    {
        public string hotel { get; set; }
        public string tarifas { get; set; }
        public string contacto { get; set; }
        public string ciudad { get; set; }
        public string servicios { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public int numStars { get; set; }
        public string paginaweb { get; set; }
        public string email { get; set; }
    }




    public class EquiposIt
    {
        [DisplayName("ITEM")]
        public string item { get; set; }
        [DisplayName("CARACTERÍSTICAS")]
        public string caracteristicas { get; set; }
        [DisplayName("TIPO - CATERGORIA")]
        public string tipoCategoria { get; set; }
        [DisplayName("COMENTARIOS")]
        public string comentarios { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DisplayName("PRECIOS 2020")]
        public decimal precios { get; set; }
    }

    public class FileUploadModel
    {
        [Required]
        public HttpPostedFileBase fileUpload { get; set; }
    }

    public class ExcelInfo
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string id_fiscal { get; set; }
        public string correo_1 { get; set; }
        public string correo_2 { get; set; }
        public string correo_3 { get; set; }



        public List<string> n_doc { get; set; }
        public List<string> fecha_emision { get; set; }
        public List<string> fecha_vencimiento { get; set; }
        public List<string> dias_credito { get; set; }
        public List<string> dias_vencido { get; set; }
        public List<string> importe_bruto { get; set; }
        public List<string> importe_pendiente { get; set; }
        public List<string> observacion { get; set; }

    }

}