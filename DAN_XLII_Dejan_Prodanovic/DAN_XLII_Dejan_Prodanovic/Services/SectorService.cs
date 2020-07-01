using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic.Services
{
    class SectorService : ISectorService
    {
        public tblSector AddSector(tblSector sector)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblSector newSector = new tblSector();

                    newSector.SectorName = sector.SectorName;
                 
                    context.tblSectors.Add(newSector);

                    context.SaveChanges();
                  
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogAddIDCardToFile(idCard);

                    sector.SectorID = newSector.SectorID;
                    return sector;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblSector GetSectorByName(string sector)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblSector sectorFromDB = (from s in context.tblSectors where s.SectorName.Equals(sector) select s).First();


                    return sectorFromDB;

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

  
    }
}
