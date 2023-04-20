using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Data.AccessJDE
{
    public class JDE
    {
        string myConnString = ConfigurationManager.ConnectionStrings["Jde_Conex"].ConnectionString;

        string environmentLibrary = ConfigurationManager.AppSettings.Get("EnvioronmentLibrary");
    }
}