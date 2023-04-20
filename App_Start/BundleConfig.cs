using System.Web;
using System.Web.Optimization;

namespace DoleEcIntranet
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
          

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js")
                        );

            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
                     
                       "~/Scripts/alertify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/SRG").Include(
                   "~/Scripts/SolicitarRembolsoGastos.js"));


            bundles.Add(new StyleBundle("~/bundles/Scripts").Include(
                  "~/Scripts/diseño.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css2").Include(
                  "~/Content/bootstrap.min.css",
                           "~/Content/jquery-ui.css",
                           "~/Scripts/Theme/css/custom.css",
                     "~/Content/select2.min.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.min.js",
                              "~/Scripts/bootstrap.min.js",
                              "~/Scripts/jquery-ui.js",


                              //  "~/Scripts/Theme/vendor/bootstrap/js/bootstrap.min.js",
                              "~/Scripts/Theme/js/theme.js",
                              "~/Scripts/Theme/vendor/jquery.appear/jquery.appear.min.js",
                              "~/Scripts/Theme/vendor/jquery.easing/jquery.easing.min.js",
                              "~/Scripts/Theme/vendor/jquery-cookie/jquery-cookie.min.js",
                       //       "~/Scripts/Theme/vendor/popper/umd/popper.min.js",
                              "~/Scripts/Theme/vendor/common/common.min.js",
                              "~/Scripts/Theme/vendor/jquery.validation/jquery.validation.min.js",
                              "~/Scripts/Theme/vendor/jquery.easy-pie-chart/jquery.easy-pie-chart.min.js",
                              "~/Scripts/Theme/vendor/jquery.gmap/jquery.gmap.min.js",
                              "~/Scripts/Theme/vendor/jquery.lazyload/jquery.lazyload.min.js",
                              "~/Scripts/Theme/vendor/isotope/jquery.isotope.min.js",
                              "~/Scripts/Theme/vendor/owl.carousel/owl.carousel.min.js",
                              "~/Scripts/Theme/vendor/magnific-popup/jquery.magnific-popup.min.js",
                              "~/Scripts/Theme/vendor/vide/vide.min.js",
                              "~/Scripts/Theme/vendor/rs-plugin/js/jquery.themepunch.tools.min.js",
                              "~/Scripts/Theme/vendor/rs-plugin/js/jquery.themepunch.revolution.min.js",
                              "~/Scripts/Theme/js/custom.js",
                              "~/Scripts/Theme/js/theme.init.js",
                               "~/Scripts/jquery.blockUI.js",
                               "~/Scripts/jquery.form.min.js",
                               "~/Scripts/select2.min.js",
                                 "~/Scripts/toastr.min.js",
                                 "~/Scripts/moment.min.js",
                                 "~/Scripts/DataTables/jquery.dataTables.min.js",
                                 "~/Scripts/DataTableCriollo.js"

                              ));


            bundles.Add(new StyleBundle("~/Content/css3").Include(
          "~/Scripts/Theme/css/theme.css",
                      "~/Scripts/Theme/css/theme-elements.css",
                      "~/Scripts/Theme/css/theme-blog.css",
                      "~/Scripts/Theme/css/theme-shop.css",
                     "~/Scripts/Theme/vendor/rs-plugin/css/settings.css",
                     "~/Scripts/Theme/vendor/rs-plugin/css/layers.css",
                     "~/Scripts/Theme/vendor/rs-plugin/css/navigation.css",
                      //   "~/Scripts/Theme/css/skins/skin-corporate-5.css",
                      "~/Scripts/Theme/css/skins/skin-corporate-hosting.css"
           ));


            bundles.Add(new StyleBundle("~/Content/css4").Include(
                            "~/Scripts/Theme/vendor/bootstrap/css/bootstrap.min.css",
              "~/Scripts/Theme/vendor/font-awesome/css/fontawesome-all.min.css",

                            "~/Scripts/Theme/vendor/animate/animate.min.css"
           


                     ));
            bundles.Add(new StyleBundle("~/Content/css").Include(



       "~/Scripts/Theme/vendor/simple-line-icons/css/simple-line-icons.min.css",
              "~/Scripts/Theme/vendor/owl.carousel/assets/owl.carousel.min.css",





              "~/Scripts/Theme/vendor/owl.carousel/assets/owl.theme.default.min.css",
              "~/Scripts/Theme/vendor/magnific-popup/magnific-popup.min.css"
              ));

            bundles.Add(new StyleBundle("~/bundles/alertify/css").Include(
         "~/Content/alertifyjs/alertify.min.css",
      
           "~/Content/alertifyjs/themes/default.min.css",
    
            "~/Content/toastr.min.css"


         ));


            bundles.Add(new StyleBundle("~/bundles/Date/css").Include(

                     "~/Content/themes/base/jquery-ui.min.css"

                     ));


            bundles.Add(new ScriptBundle("~/bundles/texteditor").Include(
               "~/Scripts/tinymce/tinymce.min.js"
           ));


            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                 "~/Content/DataTables/css/jquery.dataTables.min.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/datepicker/css").Include(
              "~/Content/bootstrap-datepicker.min.css"
              ));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
              "~/Scripts/bootstrap-datepicker.min.js",
              "~/Scripts/locales/bootstrap-datepicker.es.min.js"

              ));

            bundles.Add(new StyleBundle("~/bundles/Toastr/css").Include(
                "~/Content/toastr.min.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
              "~/Scripts/DataTables/jquery.dataTables.min.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/Toastr").Include(
             "~/Scripts/toastr.min.js"

             ));
            bundles.Add(new ScriptBundle("~/bundles/slippago").Include(
                "~/Scripts/SlipPago.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/slippagoadmin").Include(
               "~/Scripts/SlipPagoAdmin.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/imptorta").Include(
                "~/Scripts/ImptoRta.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/add").Include(
                "~/Scripts/add.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/calendarspicker/css").Include(
               "~/Scripts/plugins/calendarspicker/css/humanity.calendars.picker.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/calendarspicker").Include(
                "~/Scripts/plugins/calendarspicker/js/jquery.calendars.min.js",
                "~/Scripts/plugins/calendarspicker/js/jquery.calendars.plus.min.js",
                "~/Scripts/plugins/calendarspicker/js/jquery.plugin.min.js",
                "~/Scripts/plugins/calendarspicker/js/jquery.calendars.picker.js", //Se modifica para ajustar la formula del calculo de la semana
                "~/Scripts/plugins/calendarspicker/js/jquery.calendars-es.js",
                "~/Scripts/plugins/calendarspicker/js/jquery.calendars.picker-es.js"


                ));

            bundles.Add(new StyleBundle("~/bundles/bootstrapselect/css").Include(
              "~/Content/bootstrap-select.min.css"
              ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapselect").Include(
             "~/Scripts/bootstrap-select.min.js"
             ));


        }
    }
}
