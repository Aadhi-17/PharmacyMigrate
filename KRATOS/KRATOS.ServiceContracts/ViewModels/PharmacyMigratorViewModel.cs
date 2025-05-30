using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRATOS.ServiceContracts
{
    public class PharmacyMigratorViewModel
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string Environment { get; set; }
        public string stagingConnectionString { get; set; }
        public string MySQLConnectionString { get; set; }

        public PharmacyMigratorViewModel()
        {
            stagingConnectionString = null;
            MySQLConnectionString  = null;
        }
    }
}
