using DoleEcIntranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DoleEcIntranet.Tools
{
    public class Tools
    {
        public static ByteArrayContent GetContentRequest(string content)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;

        }

        public static List<DtoGeneric> FillCombo()
        {
            List<DtoGeneric> model = new List<DtoGeneric>();


            model.Add(new DtoGeneric() { Id = 972, Name = "Eloisa" });
            model.Add(new DtoGeneric() { Id = 955, Name = "Lolas" });
            model.Add(new DtoGeneric() { Id = 950, Name = "Elbas" });
            model.Add(new DtoGeneric() { Id = 1141, Name = "Banaloli 1" });
            model.Add(new DtoGeneric() { Id = 1407, Name = "Isabel Maria 1" });
            model.Add(new DtoGeneric() { Id = 961, Name = "Josefa" });
            model.Add(new DtoGeneric() { Id = 1890, Name = "Blanca Rosa" });
            model.Add(new DtoGeneric() { Id = 1313, Name = "Maria Jose 1" });
            model.Add(new DtoGeneric() { Id = 2313, Name = "Maria Jose 2" });
            model.Add(new DtoGeneric() { Id = 3407, Name = "Isabel Maria 3" });

            return model;




        }
    }
}