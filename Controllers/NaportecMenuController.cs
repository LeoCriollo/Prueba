using DoleEcIntranet.Models;
using DoleEcIntranet.Tools;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoleEcIntranet.Controllers
{
    public class NaportecMenuController : AsyncController
    {


        #region Menu Naportec
        [HttpGet]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolEnvioCorreo)]
        public ActionResult IndexEnvio()
        {
            return View();
        }

        [HttpPost]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolEnvioCorreo)]
        [ValidateAntiForgeryToken]
        [AsyncTimeout(600000)]
        public JsonResult IndexEnvio(FileUploadModel file)
        {
            string message = "";
            if (ModelState.IsValid)
            {

                try
                {

                    // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excelPackage = new ExcelPackage(file.fileUpload.InputStream))
                    {
                        List<ExcelInfo> listaInfo = new List<ExcelInfo>();

                        ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                        ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();

                        var start = excelWorksheet.Dimension.Start;
                        var end = excelWorksheet.Dimension.End;

                        for (int row = start.Row + 1; row <= end.Row; row++)
                        {
                            ExcelInfo line = new ExcelInfo();

                            var lineL = listaInfo.FirstOrDefault(a => a.codigo.Equals(excelWorksheet.Cells[row, 1].Text));
                            if (lineL == null)
                            {
                                line.n_doc = new List<string>();
                                line.fecha_emision = new List<string>();
                                line.fecha_vencimiento = new List<string>();
                                line.dias_credito = new List<string>();
                                line.dias_vencido = new List<string>();
                                line.importe_bruto = new List<string>();
                                line.importe_pendiente = new List<string>();
                                line.observacion = new List<string>();

                                line.codigo = excelWorksheet.Cells[row, 1].Text;
                                line.nombre = excelWorksheet.Cells[row, 2].Text;
                                line.id_fiscal = excelWorksheet.Cells[row, 3].Text;
                                line.correo_1 = excelWorksheet.Cells[row, 13].Text;
                                line.correo_2 = excelWorksheet.Cells[row, 14].Text;
                                line.correo_3 = excelWorksheet.Cells[row, 15].Text;

                                line.n_doc.Add(excelWorksheet.Cells[row, 4].Text);
                                line.fecha_emision.Add(excelWorksheet.Cells[row, 5].Text);
                                line.fecha_vencimiento.Add(excelWorksheet.Cells[row, 6].Text);
                                line.dias_credito.Add(excelWorksheet.Cells[row, 7].Text);
                                line.dias_vencido.Add(excelWorksheet.Cells[row, 8].Text);
                                line.importe_bruto.Add(excelWorksheet.Cells[row, 9].Text);
                                line.importe_pendiente.Add(excelWorksheet.Cells[row, 10].Text);
                                line.observacion.Add(excelWorksheet.Cells[row, 11].Text);

                                listaInfo.Add(line);

                            }
                            else
                            {
                                lineL.n_doc.Add(excelWorksheet.Cells[row, 4].Text);
                                lineL.fecha_emision.Add(excelWorksheet.Cells[row, 5].Text);
                                lineL.fecha_vencimiento.Add(excelWorksheet.Cells[row, 6].Text);
                                lineL.dias_credito.Add(excelWorksheet.Cells[row, 7].Text);
                                lineL.dias_vencido.Add(excelWorksheet.Cells[row, 8].Text);
                                lineL.importe_bruto.Add(excelWorksheet.Cells[row, 9].Text);
                                lineL.importe_pendiente.Add(excelWorksheet.Cells[row, 10].Text);
                                lineL.observacion.Add(excelWorksheet.Cells[row, 11].Text);

                            }

                        }


                        foreach (var item in listaInfo)
                        {
                            string dias = "0";

                            string body = "<p>";

                            body += "Estimados";
                            body += "</p>";

                            body += "<p>";
                            body += item.nombre + "(Id. Fiscal : " + item.id_fiscal + ")";
                            body += "</p>";

                            body += "<p>";
                            body += "A continuación, sírvase encontrar su estado de cuenta con el correspondiente detalle de facturas registradas:";
                            body += "</p>";       
                            
                            body += "</br>";
                          
                            body += "<table class='table' cellspacing='0' cellpadding='1' style='border - collapse: collapse; ' >";
                            body += "<thead>";
                            body += "<tr style='background-color: rgb(216, 216, 216);'>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Código</th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Num Documento</th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Fecha Emisión</th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Fecha Vencimiento</ th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Días Crédito</ th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Días Vencido</th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Importe Bruto</th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Importe Pendiente</th>";
                            body += "<th style='text-align: center; width: 100px; border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>Observación</th>";
                            body += "</tr>";
                            body += "</thead>";
                            body += "<tbody>";

                            body += "<tr style='background-color: rgb(255, 255, 255);'>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            body += item.codigo;
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                                               
                            foreach (var itemd in item.n_doc)
                            {
                                body += itemd + "<br/>";

                            }
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.fecha_emision)
                            {
                                body += itemd + "<br/>";
                            }
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.fecha_vencimiento)
                            {
                                body += itemd + "<br/>";
                            }
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.dias_credito)
                            {
                                body += itemd + "<br/>";
                                dias = itemd;
                            }
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.dias_vencido)
                            {
                                body += itemd + "<br/>";
                            }
                            body += "</td>";


                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.importe_bruto)
                            {
                                body += itemd + "<br/>";
                            }
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.importe_pendiente)
                            {
                                body += itemd + "<br/>";
                            }
                            body += "</td>";

                            body += "<td style='text-align: center;border-width: 1px; border-style: solid; border-color: rgb(171, 171, 171);'>";
                            foreach (var itemd in item.observacion)
                            {
                                body += itemd + "<br/>";
                            }
                            body += "</td>";
                            body += "</tr>";
                            body += "</tbody>";
                            body += "</table>";
                            body += "</br></br>";

                            body += "<p>";
                            body += string.Format("{0}{1}{2}", "Le recordamos que el término de pago de cada factura es de ", dias,
                                " días calendario a partir de su fecha de emisión, de antemano agradecemos vuestra revisión y gestión.");
                            body += "</p>";
                            body += "</br>";
                            body += "<p><b>Las retenciones quedan registradas en un plazo de 72 horas. En caso de haber enviado ya su retención al momento de recibir el estado de cuenta, favor hacer caso omiso.</b></p>";
                            body += "</br>";
                            body += "<p>Para mayor consultas favor comunicarse con:</p>";
                            body += "<p>Ing. Jonathan Dávila Pesantes | Analista Senior de Crédito y Cobranza</p>";
                            body += "<p>Teléfono: (+593 4) 6010230 Ext.:4311   |   Celular: (+593) 95 967 2741   |   E-mail: jonathan.davila@dole.com</p>";
                            body += "</br>";
                            body += "<p><b>Atentamente,</b></p>";
                            body += "<p><b>Crédito y Cobranzas</b></p>";
                            body += "<p><b>NAPORTEC S.A.</b></p>";
                            body += "</br></br>";
                            body += "<p><i>NOTA: Este es un mail automático, favor no contestar.</i></p>";

                            List<string> cc = new List<string>();
                            cc.Add(item.correo_2);
                            cc.Add(item.correo_3);
                            cc.Add("jonathan.davila@dole.com");
                      

                            //Envis el email.
                            Mail.Send(item.correo_1, cc, "Naportec - Estado de cuenta al " + DateTime.Now.ToShortDateString(), body);
                        }
                                                
                    }
                }catch(Exception ex)
                {
                    message = ex.Message;
                }
            }
            else
            {
                message = "Error archivo a escogido un archivo incorrecto";
            }
            
            return Json(message, JsonRequestBehavior.DenyGet);
        }


        [HttpGet]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolReportesNaportec)]
        public ActionResult DespachoImportacion()
        {
            return View();
        }

        [HttpGet]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolReportesNaportec)]
        public ActionResult EmbarqueExportacion()
        {
            return View();
        }

        [HttpGet]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolReportesNaportec)]
        public ActionResult AlmaDepositoTemp()
        {
            return View();
        }

        [HttpGet]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolReportesNaportec)]
        public ActionResult DespachoExportacion()
        {
            return View();
        }




        #endregion



    }
}
