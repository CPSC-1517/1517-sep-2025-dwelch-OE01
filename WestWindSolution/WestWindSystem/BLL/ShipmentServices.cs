using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class ShipmentServices
    {
        #region setup of the context connection variable and class constructor
        private readonly WestWindContext _context;

        internal ShipmentServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Query Services

        public List<Shipment> Shipment_GetByYearandMonth(int year, int month)
        {
            //    it is possible to place validation of incoming parameters within your services
            //    remember the services are independent of the outside user
            if(year < 1950 || year > DateTime.Today.Year)
            {
                throw new ArgumentException($"Invalid year {year}. Year must be between 1950 and today.");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Invalid month {month}. Month must be between 1 and 12.");
            }

            //obtain the database info
            IEnumerable<Shipment> info = _context.Shipments
                                                .Where(x => x.ShippedDate.Year == year
                                                        && x.ShippedDate.Month == month)
                                                .OrderBy(x => x.ShippedDate);

            //remember to convert your IEnumberale datatype to List
            return info.ToList();
        }
        #endregion
    }
}
