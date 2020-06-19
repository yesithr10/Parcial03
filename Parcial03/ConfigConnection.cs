using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Parcial03
{
    public static class ConfigConnection
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static string ProviderName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
    }
}
