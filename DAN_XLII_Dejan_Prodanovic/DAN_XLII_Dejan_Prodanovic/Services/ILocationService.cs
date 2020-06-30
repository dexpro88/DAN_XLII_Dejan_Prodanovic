using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic.Services
{
    interface ILocationService
    {
        tblLOCATION AddLocation(tblLOCATION location);
        List<vwLOCATION> GetAllLocations();
    }
}
