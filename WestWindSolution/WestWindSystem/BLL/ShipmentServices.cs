using Microsoft.EntityFrameworkCore;
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

        //public List<Shipment> Shipment_GetByYearandMonth(int year, int month)
        //{
        //    //    it is possible to place validation of incoming parameters within your services
        //    //    remember the services are independent of the outside user
        //    if(year < 1950 || year > DateTime.Today.Year)
        //    {
        //        throw new ArgumentException($"Invalid year {year}. Year must be between 1950 and today.");
        //    }
        //    if (month < 1 || month > 12)
        //    {
        //        throw new ArgumentException($"Invalid month {month}. Month must be between 1 and 12.");
        //    }

        //    //obtain the database info
        //    IEnumerable<Shipment> info = _context.Shipments
        //                                        .Where(x => x.ShippedDate.Year == year
        //                                                && x.ShippedDate.Month == month)
        //                                        .OrderBy(x => x.ShippedDate);

        //    //remember to convert your IEnumberale datatype to List
        //    return info.ToList();
        //}

        //This uses the technique (b) discussed on the ShipmentTable page
        //note there is a required using class, see Additional namespaces above.
        //uses the .Include method to add navigational instances to the return record
        //note the predicate uses the virtual navigational property of the Shipment entity
        //This will include the associated record from the Shippers table (parent) for
        //      the shipment record (child)
        public List<Shipment> Shipment_GetByYearandMonth(int year, int month)
        {
            //    it is possible to place validation of incoming parameters within your services
            //    remember the services are independent of the outside user
            if (year < 1950 || year > DateTime.Today.Year)
            {
                throw new ArgumentException($"Invalid year {year}. Year must be between 1950 and today.");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Invalid month {month}. Month must be between 1 and 12.");
            }

            //obtain the database info
            //This uses the technique (b) discussed on the ShipmentTable page
            //note there is a required using class, see Additional namespaces above.
            //uses the .Include method to add navigational instances to the return record
            //note the predicate uses the virtual navigational property of the Shipment entity
            //This will include the associated record from the Shippers table (parent) for
            //      the shipment record (child)

            //sql:  from tableA inner join tableB on tableA.fkey = tableB.pkey
            IEnumerable<Shipment> info = _context.Shipments
                                                .Include(x => x.ShipViaNavigation)
                                                .Where(x => x.ShippedDate.Year == year
                                                        && x.ShippedDate.Month == month)
                                                .OrderBy(x => x.ShippedDate);

            //remember to convert your IEnumberale datatype to List
            return info.ToList();
        }

        //Pagination queries
        //return the total number of records that would be returned for the query
        //this query will NOT return any actual query result records
        public int Shipment_GetByYearandMonthCount(int year, int month)
        {
            if (year < 1950 || year > DateTime.Today.Year)
            {
                throw new ArgumentException($"Invalid year {year}. Year must be between 1950 and today.");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Invalid month {month}. Month must be between 1 and 12.");
            }

            //execute the query without any additional methods use to join other tables or organize the 
            //   queried dataset
            //use .Count() to obtain the number of rows
            int info = _context.Shipments
                            .Where(x => x.ShippedDate.Year == year
                                    && x.ShippedDate.Month == month)
                            .Count();

            return info;

            //return _context.Shipments
            //                .Where(x => x.ShippedDate.Year == year
            //                        && x.ShippedDate.Month == month)
            //                .Count();
        }

        //this method will return the data set records that are NEEDED for the current page
        //it does NOT return the entire data set collection
        //the method needs to determine the record subset to return
        //the currentpagenumber and itemsperpage will be use to select the data subset of rows.
        public List<Shipment> Shipment_GetByYearandMonthPaging(int year, int month,
                                                            int currentpagenumber, int itemsperpage)
        {
            if (year < 1950 || year > DateTime.Today.Year)
            {
                throw new ArgumentException($"Invalid year {year}. Year must be between 1950 and today.");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Invalid month {month}. Month must be between 1 and 12.");
            }

            //even for paging you still need the complete query data set
            //  in the organization of all records
            IEnumerable<Shipment> info = _context.Shipments
                                                .Include(x => x.ShipViaNavigation)
                                                .Where(x => x.ShippedDate.Year == year
                                                        && x.ShippedDate.Month == month)
                                                .OrderBy(x => x.ShippedDate);

            //paging calculations
            //calculate the number of records to skip
            //subtract 1 from the natural page number (currentpagenumber) to get the page index number
            int recordsSkipped = itemsperpage * (currentpagenumber - 1);

            //return JUST the records for the page
            //Skip: skip the first x items representing previous pages
            //Take: take up to the necessary number of items on a page

            return info.Skip(recordsSkipped).Take(itemsperpage).ToList();
        }
        #endregion
    }
}
