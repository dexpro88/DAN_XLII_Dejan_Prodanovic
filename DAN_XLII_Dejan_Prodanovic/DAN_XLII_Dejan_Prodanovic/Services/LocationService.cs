using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic.Services
{
    class LocationService: ILocationService
    {
        /// <summary>
        /// method that location to database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public tblLOCATION AddLocation(tblLOCATION location)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {

                    tblLOCATION newLocation = new tblLOCATION();
                    newLocation.Adress = location.Adress;
                    newLocation.Country = location.Country;
                    newLocation.Place = location.Place;

                    context.tblLOCATIONs.Add(newLocation);
                    context.SaveChanges();
                    location.LocationID = newLocation.LocationID;

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogAddLocationToFile(location);

                    return location;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// method that takes all locations from database and returns list of locations
        /// we use vwLOCATION which has one field that contains Country,Place and Adress
        /// </summary>
        /// <returns></returns>
        public List<vwLOCATION> GetAllLocations()
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<vwLOCATION> list = new List<vwLOCATION>();
                    list = (from x in context.vwLOCATIONs select x).ToList();
                    return list;
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
