using DoleEcIntranet.Data;
using DoleEcIntranet.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System;
using System.Globalization;
using DoleEcIntranet.Data.DataAdam;
using System.Configuration;
using OfficeOpenXml;
using DoleEcIntranet.Tools;

namespace DoleEcIntranet.Controllers
{
    public class HomeController : Controller
    {

        private EntityIntranet db = new EntityIntranet();
        private EntityAdam dba = new EntityAdam();


        public ActionResult Index()
        {
            //this.getClaveAcceso();
            return View();
        }


        public ActionResult DoleEcuador()
        {
            return View();
        }

        public ActionResult NoticiasDole()
        {
            return View();
        }


        public ActionResult NoticiasDale()
        {
            return View();
        }

        public ActionResult FundacionDale()
        {
            return View();
        }

        public ActionResult ShowOrganigrama()
        {
            return View();
        }

        public ActionResult IdentidadCorporativa()
        {
            return View();
        }

        public ActionResult Certificaciones()
        {
            return View();
        }

        public ActionResult ManualOperativoTraza()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }

        public ActionResult Empresas()
        {
            return View();
        }

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.ReporteCompras)]
        public ActionResult BiCompras()
        {           
            return View();
        }

        public ActionResult NuevoColaboradorMes()
        {
            List<ColaboradoresIngresoMes> model = new List<ColaboradoresIngresoMes>();

            int yearActual = DateTime.Now.Year;
            int monthActual = DateTime.Now.Month;

            model = dba.Rol_usuarios_ADAM.Where(w => w.status.Equals("A") && w.fh_priming.HasValue
                                                    && w.fh_priming.Value.Year == yearActual
                                                    && w.fh_priming.Value.Month == monthActual)
                                                    .Select(s => new ColaboradoresIngresoMes()
                                                    {
                                                        codEmpleado = s.num_personal,
                                                        nombreCia = s.nom_cia.TrimEnd(),
                                                        trabajador = s.nombre.TrimEnd(),
                                                        fechaIngreso = s.fh_priming.Value,
                                                        fechaInicio = s.fh_priming.Value

                                                    }).ToList();

            return View(model);
        }

        public ActionResult ReclutamientoInterno()
        {

            ReclutamientoInternoViewModel model = null;
            DateTime fechActual = DateTime.Now;
            TimeSpan ts = new TimeSpan(00, 00, 0);
            fechActual = fechActual.Date + ts;

            var reclutamiento = db.Contenidos.FirstOrDefault(f => f.IdCategoria == (int)Tools.Enum.Categorias.Reclutamiento_Interno
             && f.FechaPublicacion.Value <= fechActual
             && f.FechaExpiracion.Value >= fechActual);

            if (reclutamiento != null)
                model = new ReclutamientoInternoViewModel() { Contenido = reclutamiento.Contenido2 };


            return View(model);
        }



        public ActionResult CarteraHoteles()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult GetCiudadesHoteles(string[] filter)
        {

            List<CiudadHoteles> model = new List<CiudadHoteles>();

            if (filter != null && filter.Length > 0)
            {

                for (int i = 0; i < filter.Length; ++i)
                {

                    if (filter[i].Equals("1"))
                    {
                        CiudadHoteles ch1 = new CiudadHoteles()
                        {
                            ciudad = "Cuenca",
                            hoteles = "FOUR POINTS BY SHERATON",
                            habitacionSencilla = "$ 75.00",
                            email = "reservas@fourpointscuenca.com",
                            telefono = "(07) 6022000",
                            codHotel = "ch1"
                        };
                        model.Add(ch1);
                        CiudadHoteles ch2 = new CiudadHoteles()
                        {
                            ciudad = "Cuenca",
                            hoteles = "HOTEL ORO VERDE CUENCA",
                            habitacionSencilla = "$ 94.00",
                            email = "reservas_cue@oroverdehotels.com",
                            telefono = "(07) 4090000 ext 136",
                            codHotel = "ch2"
                        };
                        model.Add(ch2);
                    }
                    else if (filter[i].Equals("2"))
                    {
                        CiudadHoteles gh1 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL EL ESCALÓN",
                            habitacionSencilla = "$ 50.00",
                            email = "reservas@escalonhotel.com",
                            telefono = "(04) 2388239",
                            codHotel = "gh1"
                        };
                        model.Add(gh1);
                        CiudadHoteles gh2 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "GRAND HOTEL GUAYAQUIL",
                            habitacionSencilla = "$ 55.00",
                            email = "reservas@grandhotelguayaquil.com",
                            telefono = "(04) 2329690",
                            codHotel = "gh2"
                        };
                        model.Add(gh2);
                        CiudadHoteles gh3 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL PALACE",
                            habitacionSencilla = "$ 55.00",
                            email = "mercadeo2@hotelpalaceguayaquil.com.ec",
                            telefono = "(04) 2321080",
                            codHotel = "gh3"
                        };
                        model.Add(gh3);
                        CiudadHoteles gh4 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL CONTINENTAL",
                            habitacionSencilla = "$ 75.00",
                            email = "reservas@hotelcontinental.com.ec",
                            telefono = "(04) 2329270",
                            codHotel = "gh4"
                        };
                        model.Add(gh4);
                        CiudadHoteles gh5 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL RADISSON",
                            habitacionSencilla = "$ 60.00",
                            email = "marcela.alejandro@ghlhoteles.com",
                            telefono = "(04) 6008084",
                            codHotel = "gh5"
                        };
                        model.Add(gh5);
                        CiudadHoteles gh6 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL ORO VERDE GUAYAQUIL",
                            habitacionSencilla = "$ 50.00",
                            email = "reservas_gye@oroverdehotels.com",
                            telefono = "(04) 3811000",
                            codHotel = "gh6"
                        };
                        model.Add(gh6);
                        CiudadHoteles gh7 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL WYNDHAM GARDEN GUAYAQUIL",
                            habitacionSencilla = "$ 70.00",
                            email = "reservas@wyndhamgardenguayaquil.com",
                            telefono = "(04) 3713690",
                            codHotel = "gh7"
                        };
                        model.Add(gh7);
                        CiudadHoteles gh8 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL SHERATON",
                            habitacionSencilla = "$ 60.00",
                            email = "reservas@sheratonguayaquil.com",
                            telefono = "(04) 3707070",
                            codHotel = "gh8"
                        };
                        model.Add(gh8);
                        CiudadHoteles gh9 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL WYNDHAM",
                            habitacionSencilla = "$ 99.00",
                            email = "reservas@wyndhamguayaquil.com",
                            telefono = "(04) 3717800",
                            codHotel = "gh9"
                        };
                        model.Add(gh9);
                        CiudadHoteles gh10 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL HILTON COLON",
                            habitacionSencilla = "$ 115.00",
                            email = "reservas@hiltonguayaquil.com",
                            telefono = "(04) 5010000",
                            codHotel = "gh10"
                        };
                        model.Add(gh10);
                        CiudadHoteles gh11 = new CiudadHoteles()
                        {
                            ciudad = "Guayaquil",
                            hoteles = "HOTEL DEL PARQUE",
                            habitacionSencilla = "$ 243.00",
                            email = "reservas_parque@oroverdehotels.com",
                            telefono = "(04) 5000111",
                            codHotel = "gh11"
                        };
                        model.Add(gh11);
                    }
                    else if (filter[i].Equals("3"))
                    {
                        CiudadHoteles mh1 = new CiudadHoteles()
                        {
                            ciudad = "Machala",
                            hoteles = "VEUXOR",
                            habitacionSencilla = "$ 62.09",
                            email = "reservas@hotelveuxor.com.ec",
                            telefono = "(07) 2932428",
                            codHotel = "mh1"
                        };
                        model.Add(mh1);
                        CiudadHoteles mh2 = new CiudadHoteles()
                        {
                            ciudad = "Machala",
                            hoteles = "HOTEL ORO VERDE MACHALA",
                            habitacionSencilla = "$ 94.00",
                            email = "ov_mch@oroverdehotels.com",
                            telefono = "(07) 2980074",
                            codHotel = "mh2"
                        };
                        model.Add(mh2);

                    }
                    else if (filter[i].Equals("4"))
                    {
                        CiudadHoteles mah1 = new CiudadHoteles()
                        {
                            ciudad = "Manta",
                            hoteles = "HOTEL ORO VERDE MANTA",
                            habitacionSencilla = "$ 105.00",
                            email = "reservas_mta@oroverdehotels.com",
                            telefono = "(05) 2629200",
                            codHotel = "mah1"
                        };
                        model.Add(mah1);

                    }
                    else if (filter[i].Equals("5"))
                    {
                        CiudadHoteles qah1 = new CiudadHoteles()
                        {
                            ciudad = "Quito",
                            hoteles = "AKROS",
                            habitacionSencilla = "$ 78.00",
                            email = "reservaciones@hotelakros.com",
                            telefono = "(02) 2430600",
                            codHotel = "qah1"
                        };
                        model.Add(qah1);
                        CiudadHoteles qah2 = new CiudadHoteles()
                        {
                            ciudad = "Quito",
                            hoteles = "WYNDHAM GARDEN QUITO",
                            habitacionSencilla = "$ 80.00",
                            email = "reservas@wyndhamgardenquito.com",
                            telefono = "(02) 2265265",
                            codHotel = "qah2"
                        };
                        model.Add(qah2);
                        CiudadHoteles qah3 = new CiudadHoteles()
                        {
                            ciudad = "Quito",
                            hoteles = "HOTEL WYNDHAM QUITO AIRPORT",
                            habitacionSencilla = "$ 119.00",
                            email = "reservas@wyndhamquito.com",
                            telefono = "(02) 3958000",
                            codHotel = "qah3"
                        };
                        model.Add(qah3);

                    }
                }
            }
            return PartialView("_GetCiudadesHoteles",model);
        }


        [HttpPost]
        public PartialViewResult DetalleHotel(HotelConsult filter)
        {
            HotelDetalle model = new HotelDetalle();
            if (filter.codigo.Equals("ch1"))
            {
                model.hotel = "FOUR POINTS BY SHERATON";               
                model.contacto = "Diana Caicedo";
                model.tarifas = "Habitación Classic King o Twin: $75.00 | Habitación Classic King o Twin City View: $85.00 | Suite Sencilla/Doble: $160.00";
                model.ciudad = "Cuenca";
                model.servicios = "Desayuno Buffet en el Restaurante Cook´s, transfer Aeropuerto/Hotel/Aeropuerto (previa coordinación), acceso al Fitness Center (piscina, sauna, turco, jacuzzi, gimnasio), Business Center, periódico local diario, asistencia médica 24 horas.";
                model.telefono = "(07) 6022000";
                model.direccion = "Av. Circunvalación Sur y y Av. Felipe II (Junto a Mall del Rio)";
                model.paginaweb = "https://www.marriott.com/hotels/hotel-photos/cuefp-four-points-cuenca/";
                model.numStars = 3;
                model.email = "reservas@fourpointscuenca.com";
            }else if(filter.codigo.Equals("ch2"))
            {
                model.hotel = "HOTEL ORO VERDE CUENCA";
                model.contacto = "Ivan Salazar";
                model.tarifas = "Habitación Sencilla: $89.00  | Habitación Doble: $99.00  |  Suite Oro Verde: $129.00";
                model.ciudad = "Cuenca";
                model.servicios = "Desayuno Buffet incluido, traslado Aeropuerto/Hotel/Aeropuerto con WiFi, Internet Wireless, asistencia médica 24 horas, acceso a nuestro Oro Fit, gimnasio, sauna, jacuzzi y baño polar, estacionamiento gratuito, periódico local (previa solicitud), caja de seguridad";
                model.telefono = "(07)4090000 ext136";
                model.direccion = "Av Ordonez Lasso s/n Cuenca, Azuay 10215";
                model.paginaweb = "";
                model.numStars = 5;
                model.email = "reservas@fourpointscuenca.com";
            }
            else if (filter.codigo.Equals("gh1"))
            {
                model.hotel = "HOTEL EL ESCALÓN";
                model.contacto = "Tamara Ronquillo";
                model.tarifas = "Habitación Sencilla: $50.00                    Doble: $80.00                                        Triple: $65.00                   ";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno incluido, WiFi incluido ";
                model.telefono = "(04) 2388239";
                model.direccion = "Urdesa, 419, Manuel Rendón Seminario";
                model.paginaweb = "http://www.escalonhotel.com/rooms";
                model.numStars = 3;
                model.email = "reservas@escalonhotel.com";
            }
            else if (filter.codigo.Equals("gh2"))
            {
                model.hotel = "GRAND HOTEL GUAYAQUIL";
                model.contacto = "Jose Andres Estevez";
                model.tarifas = "Habitación Standard Sencilla: $40.00 Habitación Standard Doble: $45.00  Habitación Deluxe: $110.00                  Suite: $135.00  ";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet servido en la Cafetería La Pepa de Oro, transfer Aeropuerto/Hotel/Aeropuerto con WiFi (Previa solicitud), coctel/helado de bienvenida, WiFi ilimitado, servicio de asistencia médica 24 horas para emergencias, piscina de estilo tropical con cascada, admisión al Terraza Racquet Club: canchas de squash, gimnasio, sauna, vapor, solárium, área social";
                model.telefono = "(04) 2329690";
                model.direccion = "Boyacá 1615 y Malecon ";
                model.paginaweb = "http://www.grandhotelguayaquil.com/";
                model.numStars = 4;
                model.email = "reservas@grandhotelguayaquil.com";
            }
            else if (filter.codigo.Equals("gh3"))
            {
                model.hotel = "HOTEL PALACE";
                model.tarifas = "Habitación Sencilla: $50.00                                      Doble o Matrimonial: $55.00                           Triple: $85.00";
                model.contacto = "Kerly Roman";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido, Internet WIFI en habitaciones y áreas públicas del hotel, llamadas locales ilimitadas, periódico local, fuente de frutas y estación de café permanente en la recepción, coctel de bienvenida, caja de seguridad electrónica, servicio “Viaje sin Maletas”, servicio de lustrado de calzado";
                model.telefono = "(04) 2321080";
                model.direccion = "Chile #214 y Luque";
                model.paginaweb = "http://www.hotelpalaceguayaquil.com.ec/default-en.html";
                model.numStars = 3;
                model.email = "mercadeo2@hotelpalaceguayaquil.com.ec";
            }
            else if (filter.codigo.Equals("gh4"))
            {
                model.hotel = "HOTEL CONTINENTAL";
                model.tarifas = "Sencilla Estándar: $75.00                         Sencilla Ejecutiva: $83.00                            Sencilla Superior: $90.00                              Junior Suites: $98.00                       Triples: $122.79";
                model.contacto = "Roberto Quintero";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido en La Cafetería La Canoa, Internet WiFi en las habitaciones y áreas públicas del hotel Cuentan  con:                 Restaurant El Fortín comida internacional, Cafetería “La Canoa” comida típica 24 horas, Cocktail Lounge “El Astillero”, piqueos, tragos y cocteles, Salones de Banquetes “Los Candelabros”, Eventos a Domicilio “Servicio de Catering”";
                model.telefono = "(04) 2329270";
                model.direccion = "Chile 512 y 10 de agosto esquina";
                model.paginaweb = "https://www.hotelcontinental.com.ec/es/galeria/";
                model.numStars = 5;
                model.email = "reservas@hotelcontinental.com.ec";
            }
            else if (filter.codigo.Equals("gh5"))
            {
                model.hotel = "HOTEL RADISSON";
                model.tarifas = "Habitación Sencilla: $60.00                    Doble o Matrimonial: $70.00                                        Junior Suite: $120.00 ";
                model.contacto = "Andrea Falcones";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido, transfer Aeropuerto/Hotel/Aeropuerto, Internet ilimitado, parqueo gratuito";
                model.telefono = "(04) 6008084";
                model.direccion = "Ciudadela Kennedy, Av. Gral. Francisco Boloña 503A y Calle Jorge Insua Hindro";
                model.paginaweb = "https://www.ghlhoteles.com/hoteles/ecuador/guayaquil/radisson-hotel-guayaquil/fotos/";
                model.numStars = 4;
                model.email = "marcela.alejandro@ghlhoteles.com";
            }
            else if (filter.codigo.Equals("gh6"))
            {
                model.hotel = "HOTEL ORO VERDE GUAYAQUIL";
                model.tarifas = "Habitación Deluxe Sencilla: $50.00                                                 Habitación Deluxe Doble: $55.00                                   ";
                model.contacto = "Joyce Mosquera";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido, traslado Aeropuerto/ Hotel / Aeropuerto con WiFi, WiFi en las habitaciones y demás áreas del hotel, caja de seguridad en la habitación, acceso a nuestro Oro Fit, gimnasio, sauna, jacuzzi, piscina y baño polar.";
                model.telefono = "(04) 3811000";
                model.direccion = "Av. 9 de Octubre 414 y Garcia Moreno";
                model.paginaweb = "https://www.ghlhoteles.com/hoteles/ecuador/guayaquil/radisson-hotel-guayaquil/fotos/";
                model.numStars = 5;
                model.email = "reservas_gye@oroverdehotels.com";
            }
            else if (filter.codigo.Equals("gh7"))
            {
                model.hotel = "HOTEL WYNDHAM GARDEN GUAYAQUIL";
                model.tarifas = "Habitación Single Classic: $60.00      Habitación Double Classic: $70.00      Single Garden: $100.00                            Double Garden: $110.00                             Single Apart Suite: $120.00";
                model.contacto = "Sugey Trampuz";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido, traslado Aeropuerto/Hotel/Aeropuerto, Internet Wireless, estación de bebidas en Lobby, acceso a Business Center, Fitness Center, sauna, jacuzzi.";
                model.telefono = "(04) 3713690";
                model.direccion = "Av. Juan Tanca Marengo y Abel Romero Castillo";
                model.paginaweb = "";
                model.numStars = 4;
                model.email = "reservas@wyndhamgardenguayaquil.com";
            }
            else if (filter.codigo.Equals("gh8"))
            {
                model.hotel = "HOTEL SHERATON ";
                model.tarifas = "Superior Sencilla: $60.00              Superion Doble: $70.00               Junior Suite: $190.00                            Club Level Sencilla: $180.00      ";
                model.contacto = "Monica Emen";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido, Internet WiFi, transporte aeropuerto y parqueadero. BENEFICIOS CLUB LEVEL Desayuno Buffet incluido, Open Bar(18h00 - 21h00), estaciones permanentes de bebidas y snacks, sala de juntas ";
                model.telefono = "(04) 3707070";
                model.direccion = "Av. Constitucion s/n y Av. Juan Tanca Marengo frente al C.C Mall del Sol";
                model.paginaweb = "https://www.ghlhoteles.com/hoteles/ecuador/guayaquil/sheraton-guayaquil/fotos/";
                model.numStars = 5;
                model.email = "reservas@sheratonguayaquil.com";
            }
            else if (filter.codigo.Equals("gh9"))
            {
                model.hotel = "HOTEL WYNDHAM ";
                model.tarifas = "Habitación Deluxe Vista Colonial: $99.00                                                Habitación Premium Vista al Río: $109.00                                                                                      Junior Suite Vista al Río:               $219.00                                                        Suite Presidencial Vista al Río: $399.00                                                 *Acceso a Club Level: $160.00 ";
                model.contacto = "Maria Gracia Mariscal";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet, WiFi, transporte Aeropuerto/Hotel (previa coordinación, salidas cada hora), acceso al Fitness Center, acceso a sala de reuniones semiprivada (previa reserva), periódico local diario, asistencia médica 24 horas";
                model.telefono = "(04) 3717800";
                model.direccion = "Calle Numa Pompilio Llona S-N, Ciudad del Río - Puerto Santa Ana";
                model.paginaweb = "";
                model.numStars = 5;
                model.email = "reservas@wyndhamguayaquil.com";
            }
            else if (filter.codigo.Equals("gh10"))
            {
                model.hotel = "HOTEL HILTON COLÓN GUAYAQUIL";
                model.tarifas = "Habitación Deluxe Sencilla: $115.00        Habitación Deluxe Doble: $132.00                                                                         Piso Ejecutivo Habitación Sencilla/Doble: $175.00                                               Junior Suite: $215.00                           Suite Ejecutiva : $255.00";
                model.contacto = "Alfredo Barek";
                model.ciudad = "Guayaquil";
                model.servicios = "BENEFICIOS PISO DELUXE                                                                                               Servicio de shuttle bus Aeropuerto/Hotel/Aeropuerto (reserva previa), coctel de bienvenida, Internet ilimitado, piscina, hidromasaje, sauna, vapor, gimnasio 24/7, Business Center 24/7, room service 24/7                                                          BENEFICIOS PISO EJECUTIVO                                                                                       Registro privado de llegada y salida (lounge piso 9), Desayuno Americano Buffet*, hora del te*, Happy Hour*, 3 salas de reuniones para 15 personas (reserva previa), asistencia secretarial, exclusiva área social                                                *1 invitado sin costo";
                model.telefono = "(04) 5010000";
                model.direccion = "Av. Francisco de Orellana Mz. 111";
                model.paginaweb = "https://www3.hilton.com/en/hotels/ecuador/hilton-colon-guayaquil-GYEHIHF/accommodations/index.html";
                model.numStars = 5;
                model.email = "reservas@hiltonguayaquil.com";
            }
            else if (filter.codigo.Equals("gh11"))
            {
                model.hotel = "HOTEL DEL PARQUE";
                model.tarifas = "Deluxe Individual: $243.00 (Viernes a Domingo) $199.00 (Lunes a Jueves) Junior Suite: $378.00 (Viernes a Domingo) $330.00 (Lunes a Jueves)";
                model.contacto = "Ivan Salazar";
                model.ciudad = "Guayaquil";
                model.servicios = "Desayuno Buffet incluido, traslado Aeropuerto/Hotel/Aeropuerto con WiFi, Internet Wireless, asistencia médica 24 horas, acceso a nuestro Oro Fit, gimnasio, sauna, jacuzzi y baño polar, estacionamiento gratuito, periódico local (previa solicitud), caja de seguridad";
                model.telefono = "(04) 5000111";
                model.direccion = "Parque Historico Guayaquil km 1 1/2 via a Samborondon, Av Los Arcos s/n y Malta";
                model.paginaweb = "";
                model.numStars = 5;
                model.email = "reservas_parque@oroverdehotels.com";
            }
            else if (filter.codigo.Equals("mh1"))
            {
                model.hotel = "VEUXOR";
                model.tarifas = "Habitación Sencilla: $62.09                 Habitación Doble: $74.74                              Triple: $96.49                               Suite Presidencial: $160.71";
                model.contacto = "Adrian Ortega";
                model.ciudad = "Machala";
                model.servicios = "Desayuno Buffet en el restaurante Fuxion, garaje, gimnasio, WiFi, LCD con DirecTV, room service";
                model.telefono = "(07) 2932428";
                model.direccion = "Juan Montalvo s/n y Bolívar esq.";
                model.paginaweb = "";
                model.numStars = 3;
                model.email = "reservas@hotelveuxor.com.ec";
            }
            else if (filter.codigo.Equals("mh2"))
            {
                model.hotel = "HOTEL ORO VERDE MACHALA";
                model.tarifas = "Habitación Sencilla: $94.00     Habitación Doble: $109.00";
                model.contacto = "Ivan Salazar";
                model.ciudad = "Machala";
                model.servicios = "Desayuno Buffet incluido, Internet Wireless, asistencia médica 24 horas, acceso a nuestro Oro Fit, gimnasio, sauna, jacuzzi y baño polar, estacionamiento gratuito, periódico local (previa solicitud), caja de seguridad";
                model.telefono = "(07) 2980074";
                model.direccion = "General Thelmo Sandoval y Circunvalación Norte";
                model.paginaweb = "";
                model.numStars = 5;
                model.email = "ov_mch@oroverdehotels.com";
            }
            else if (filter.codigo.Equals("mah1"))
            {
                model.hotel = "HOTEL ORO VERDE MANTA";
                model.tarifas = "Habitación Sencilla: $105.00          Habitación Doble: $115.00";
                model.contacto = "Ivan Salazar";
                model.ciudad = "Manta";
                model.servicios = "Desayuno Buffet incluido, traslado Aeropuerto/Hotel/Aeropuerto con WiFi, Internet Wireless, asistencia médica 24 horas, acceso a nuestro Oro Fit, gimnasio, sauna, jacuzzi y baño polar, estacionamiento gratuito, periódico local (previa solicitud), caja de seguridad";
                model.telefono = "(05) 2629200";
                model.direccion = "Malecon de Manta y calle 23";
                model.paginaweb = "";
                model.numStars = 3;
                model.email = "reservas_mta@oroverdehotels.com";
            }
            else if (filter.codigo.Equals("qah1"))
            {
                model.hotel = "AKROS";
                model.tarifas = "Habitación Sencilla: $78.00                    Habitación Doble: $98.00                        Habitación Triple: $118.00                            Suite: $130.00";
                model.contacto = "Carolina Granda";
                model.ciudad = "Quito";
                model.servicios = "Desayuno Buffet Ilimitado en el “Café El Patio”, llamadas telefónicas locales ilimitadas a teléfonos fijos, Business Center, WiFi, gimnasio, servicio médico las 24 Horas, valet parking y estacionamiento, salas de reuniones";
                model.telefono = "(02) 2430600";
                model.direccion = "Av. 6 de Diciembre N34-120, Quito 170122";
                model.paginaweb = "";
                model.numStars = 4;
                model.email = " reservaciones@hotelakros.com";
            }
            else if (filter.codigo.Equals("qah2"))
            {
                model.hotel = "WYNDHAM GARDEN QUITO";
                model.tarifas = "Habitación Single Classic: $80.00      Habitación Double Classic: $90.00      Single Plus: $95.00                            Double Plus: $105.00                             Junior Suite Single: $110.00                      Junior Suite Double: $120.00";
                model.contacto = "Sugey Trampuz";
                model.ciudad = "Quito";
                model.servicios = "Desayuno Buffet en el Restaurante Tradiciones, Business Center, gimnasio, coctel de bienvenida, servicio de emergencia ambulatoria, coffee station, room service 24 horas, parqueadero cubierto, valet parking, WiFi gratuito.";
                model.telefono = "(02) 2265265";
                model.direccion = "Alemania E5-103 y Av República (esq)";
                model.paginaweb = "";
                model.numStars = 3;
                model.email = " reservas@wyndhamgardenquito.com";
            }
            else if (filter.codigo.Equals("qah3"))
            {
                model.hotel = "HOTEL WYNDHAM QUITO AIRPORT";
                model.tarifas = "Habitación Deluxe Single: $119.00      Habitación Deluxe Double: $129.00      Club Level Single: $139.00                            Club Level Double: $149.00                             Jr. Suite Single: $159.00                           Jr. Suite Double: $169.00                 Suite Single: $179.00                           Suite Double: $189.00";
                model.contacto = "Sugey Trampuz";
                model.ciudad = "Quito";
                model.servicios = "Desayuno Buffet incluido, transporte Aeropuerto/Hotel/Aeropuerto, WiFi gratuito, acceso al Fitness Center, periódico local diario, sauna, turco, jaccuzi.                                                                                                                             CLUB LEVEL/SUITES                                                                                                                      Desayuno Buffet en lounge privado, check in express, 2 horas de cortesía en salas de reuniones, bocaditos premium permanente, bebidas soft ilimitadas, vino y cerveza de la casa.";
                model.telefono = "(02) 3958000";
                model.direccion = "Parroquia Tababela, SN via Yaruquí Aeropuerto Mariscal Sucre";
                model.paginaweb = "";
                model.numStars = 4;
                model.email = "reservas@wyndhamquito.com";
            }


            return PartialView("_DatalleHotel", model);


        }




        public ActionResult MenuComedor()
        {
            ViewBag.UrlFileDocument = ConfigurationManager.AppSettings["UrlFileDocument"];
            return View();
        }


        public ActionResult NotasSeguridad()
        {
            return View();
        }


        public ActionResult NotasSalud()
        {
            return View();
        }

        public ActionResult SeguroMedico()
        {
            return View();
        }


        public ActionResult MostrarFormularios()
        {
            return View();
        }


        #region Menu Sistemas

        public ActionResult BiProyecto()
        {
            return View();
        }

        public ActionResult BiCalidad()
        {
            return View();
        }

        public ActionResult NewsIt()
        {
            return View();
        }


        public ActionResult EquiposComputo()
        {
            List<EquiposIt> model = new List<EquiposIt>();

            //leer excel y cagar datos para presentarlos
            string fileDirectory = ConfigurationManager.AppSettings.Get("DocumentIt");
            FileInfo file = new FileInfo(fileDirectory);

            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {

                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();

                var start = excelWorksheet.Dimension.Start;
                var end = excelWorksheet.Dimension.End;

                for (int row = start.Row + 3; row <= end.Row - 1; row++)
                {
                    EquiposIt line = new EquiposIt();
                    line.item = excelWorksheet.Cells[row, 1].Text;
                    line.caracteristicas = excelWorksheet.Cells[row, 2].Text;
                    line.tipoCategoria = excelWorksheet.Cells[row, 3].Text;
                    line.comentarios = excelWorksheet.Cells[row, 4].Text;
                    line.precios = decimal.Parse(excelWorksheet.Cells[row, 5].Value.ToString());

                    model.Add(line);
                }

            }

                return View(model);
        }

        #endregion




        public ActionResult GenerarXmlController(GenerarXmlModel obj)
        {



            return View();


        }
        public ActionResult ServiciosAdministrativos()
        {
            return View();
        }

        public ActionResult MantVehiculos()
        {
            return View();
        }

        public ActionResult ServicioTaxis()
        {
            return View();

        }

        public ActionResult FeriaServicios()
        {
            return View();
        }

        public ActionResult RecorridoExpresos()
        {

            return View();
        }
        public ActionResult Gimnasio()
        {

            return View();
        }

        public ActionResult CarWash()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ReporteRangos()
        {

            //     var a = GetBatchnumber(new DateTime(2019,3,27), 1755);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public static int GetWeekOfYear(DateTime fecha)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(fecha);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                fecha = fecha.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }


        public string GetBatchnumber(DateTime fecha, int empacadora)
        {

            int dia = (int)fecha.DayOfWeek + 1;// DIA 
            int semana = GetWeekOfYear(fecha);// SEMANA
            string seman = semana.ToString();


            if (semana.ToString().Length == 1)
            {
                return "3" + semana + dia + "9" + "0" + empacadora;
            }
            else
            {
                return "3" + semana.ToString().Substring(1, 1) + dia + "9" + semana.ToString().Substring(0, 1) + empacadora;
            }

        }

        [HttpPost]
        public ActionResult GenerarXml(GenerarXmlModel objectUsuario)
        {
            List<GenerarXmlModel> list = new List<GenerarXmlModel>();

            XDeclaration nn = new XDeclaration("1.0", "iso-8859-1", "no");
            XDocument document = new XDocument(nn);

            XElement autorizacion = new XElement("autorizacion");

            document.Add(autorizacion);


            //var Fecha1 = CultureInfo.InvariantCulture.DateTimeFormat;
            //var date = DateTime.ParseExact( objectUsuario.Fecha.ToString(), "dd/MM/yyyy", Fecha1);

            //string myDate = objectUsuario.Fecha.ToString();
            //string dt1 = DateTime.ParseExact(myDate, "dd-MM-yyyy hh:mm:ss:tt",
            //                                           CultureInfo.InvariantCulture).ToString();

            string[] dates = objectUsuario.Fecha.Split('/');

            DateTime date1 = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));

            //     date1 = new DateTime(2019, 8, 29);

            //ToString("dd-MM-yyyy");
            //

            autorizacion.Add(new XElement("codTipoTra", objectUsuario.cod_trans));
            autorizacion.Add(new XElement("ruc", objectUsuario.Ruc));
            autorizacion.Add(new XElement("fecha", date1.ToShortDateString().Replace('-', '/')));
            autorizacion.Add(new XElement("autOld", objectUsuario.autOld));
            autorizacion.Add(new XElement("autNew", objectUsuario.autNew));
            XElement detalles = new XElement("detalles");

            autorizacion.Add(detalles);
            foreach (GenerarXmlsDet item in objectUsuario.listaCias)
            {

                XElement detalle = new XElement("detalle");
                detalles.Add(detalle);
                detalle.Add(new XElement("codDoc", item.Cod_Doc));
                detalle.Add(new XElement("estab", item.Estab));
                detalle.Add(new XElement("ptoEmi", item.Pto_Emi));
                detalle.Add(new XElement("finOld", item.Fin_Old));
                detalle.Add(new XElement("iniNew", item.Ini_New));


            }


            string handle = Guid.NewGuid().ToString();
            TempData["id"] = handle;
            string xml = nn.ToString() + document.ToString();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml.ToString());

            TempData["xml"] = xml;

            return new JsonResult()
            {
                Data = new { FileGuid = handle, FileName = objectUsuario.Ruc + "-" + objectUsuario.autOld + "-" + objectUsuario.autNew + "-" + objectUsuario.cod_trans + ".xml" }

            };



            // string Xml = Guid.NewGuid().ToString();
            //ExcelPackage workbook = new ExcelPackage();

            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    workbook.SaveAs(memoryStream);
            //    memoryStream.Position = 0;
            //    TempData[s] = memoryStream.ToArray();
            //}
            //return new JsonResult()
            //{
            //    Data = new { FileGuid = s, FileName = "Xml.xlsx" }
            //};



        }
        [HttpGet]
        public virtual ActionResult DownloadXml(string fileGuid, string fileName)
        {
            if (TempData["id"].Equals(fileGuid))
            {
                string data = TempData["xml"] as string;

                return File(Encoding.UTF8.GetBytes(data), "xml/xml", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }


        /// <summary>
        /// Metodo para mostrar imagen de resumen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MostrarImagen(int id)
        {

            byte[] imageData = db.Contenidos.FirstOrDefault(i => i.Id == id)?.ContenidoImg;
            if (imageData != null)
            {
                return File(imageData, "image/png");
            }
            return null;
        }

        /// <summary>
        /// Cargar la Seccion dada para la pagina de inicio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CargarSeccion(int seccionCat, int pagina, bool primeraVez)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index", "Error");  //revisar
            }

            List<ArticuloViewModel> model = new List<ArticuloViewModel>();
            List<Contenidos> articulos = new List<Contenidos>();

            ////Cargar seccion 
            //if (primeraVez)
            //{
            //        articulos = db.Contenidos.Where(f => f.IdEstado == (int)Tools.Enum.Estados.Creado && f.IdCategoria == seccionCat)
            //    .OrderByDescending(x => x.FechaCreacion)
            //    .Take(Tools.Enum.articulosporSeccion).ToList();
            //}
            //else
            //{
            //    articulos = ObtenerArticulosXPag(seccionCat, pagina);
            //}


            if (articulos.Any())
            {
                foreach (var item in articulos)
                {
                    ArticuloViewModel articuloS1 = new ArticuloViewModel()
                    {
                        idArticulo = item.Id,
                        titulo = item.TituloArticulo,
                        mes = this.GetMonthLetters(item.FechaCreacion.Month),
                        dia = item.FechaCreacion.Day,
                        contenidoResumen = item.Contenido1

                    };
                    model.Add(articuloS1);
                }

            }

            //if (seccionCat == (int)Tools.Enum.Categorias.Noticias_Dole)
            //{
            return PartialView("_Noticias", model);
            //}
            //else
            //{
            //    return PartialView("CargarSeccion2", model);
            //}

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCat"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        private List<Contenidos> ObtenerArticulosXPag(int idCat, int pagina = 1)
        {

            var skipRecords = pagina * Tools.Enum.articulosporSeccion;

            return db.Contenidos.Where(f => f.IdEstado == (int)Tools.Enum.Estados.Creado && f.IdCategoria == idCat)
                .OrderByDescending(x => x.FechaCreacion)
                .Skip(skipRecords)
                .Take(Tools.Enum.articulosporSeccion).ToList();
        }


        public ActionResult MostrarArticulo(int id)
        {

            var articulo = db.Contenidos.FirstOrDefault(f => f.Id == id);

            if (articulo == null)
            {
                return RedirectToAction("Index", "Error");  // Mostrar archivo no encontrado  - revisar
            }

            ArticuloViewModel model = new ArticuloViewModel()
            {

                titulo = articulo.TituloArticulo,
                contenido = articulo.Contenido2,
                idArticulo = articulo.Id

            };

            return View("MostrarArticulo", model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private string GetMonthLetters(int month)
        {
            string mes = "";

            switch (month)
            {
                case 1:
                    {
                        mes = "ENE";
                        break;
                    }
                case 2:
                    {
                        mes = "FEB";
                        break;
                    }
                case 3:
                    {
                        mes = "MAR";
                        break;
                    }
                case 4:
                    {
                        mes = "ABR";
                        break;
                    }
                case 5:
                    {
                        mes = "MAY";
                        break;
                    }
                case 6:
                    {
                        mes = "JUN";
                        break;
                    }
                case 7:
                    {
                        mes = "JUL";
                        break;
                    }
                case 8:
                    {
                        mes = "AGO";
                        break;
                    }
                case 9:
                    {
                        mes = "SEP";
                        break;
                    }
                case 10:
                    {
                        mes = "OCT";
                        break;
                    }
                case 11:
                    {
                        mes = "NOV";
                        break;
                    }
                case 12:
                    {
                        mes = "DIC";
                        break;
                    }
            }


            return mes;

        }     



        private void getClaveAcceso()
        // 17052018
        // 07
        //  0990011419001
        // 2
        // 001
        // 030
        // 000110375
        //00057155
        //10
        {
            string clave = GenerarClaveAccesso("001", "030", "000116906", "11/12/2019", "07", "0990011419001", "2", "1", "66000011");
            string clave1 = clave;
        }



        private static string GenerarClaveAccesso(string establecimiento, string ptoEmision, string secuencial,
            string fechaEmision, string codDoc, string ruc, string ambiente, string tipoEmision, string codigNum)

        {
            string serie = (establecimiento + ptoEmision);
            string codigoNumerico = codigNum;
            string claveAccesso = fechaEmision.Replace("/", "");
            claveAccesso = (claveAccesso + codDoc + ruc + ambiente + serie + secuencial + codigoNumerico + tipoEmision);
            claveAccesso = (claveAccesso.Replace(" ", ""));
            claveAccesso = (claveAccesso + CreateDigit11(claveAccesso));
            return claveAccesso;
        }
        /// <summary>
        /// Digito Verificador Modulo 11
        /// </summary>
        /// <param name="verifierString"></param>
        /// <returns></returns>
        private static string CreateDigit11(string verifierString)
        {
            int baseMax = 7;
            int multiplicador = 2;
            int total = 0;
            int verificador = 0;
            int numAux = 0;

            string[] substrings = System.Text.RegularExpressions.Regex.Split(verifierString, "");

            for (int i = substrings.Length - 1; i >= 1; i--)
                if (substrings[i] != "")
                {
                    if (multiplicador > baseMax)
                        multiplicador = 2;

                    numAux = int.Parse(substrings[i]);
                    total = total + (numAux * multiplicador);
                    multiplicador = multiplicador + 1;
                }

            verificador = 11 - (total % 11);

            if (verificador == 10)
                verificador = 1;
            else
                if (verificador == 11)
                verificador = 0;

            return verificador.ToString();
        }



    }
}