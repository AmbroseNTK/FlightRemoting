using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Server.BL;
using SharedLibrary;

namespace ApplicationRESTfulAPIs.Controllers
{
    public class FlightController : ApiController
    {
        FlightBUS flightBUS;
        public FlightController()
        {
            flightBUS = new FlightBUS();
        }
        // GET: api/Flight
        public IEnumerable<Flight> Get()
        {
            return flightBUS.SelectAll();
        }

        // POST: api/Flight
        public void Post([FromBody]Flight flight)
        {
            flightBUS.Insert(flight);
        }

        // PUT: api/Flight
        public void Put([FromBody]Flight flight)
        {
            flightBUS.Update(flight);
        }

        // DELETE: api/Flight/5
        public void Delete(int id)
        {
            flightBUS.Delete(id);
        }
    }
}
