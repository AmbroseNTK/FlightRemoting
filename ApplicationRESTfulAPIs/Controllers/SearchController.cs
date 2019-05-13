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
    public class SearchController : ApiController
    {
        FlightBUS flightBUS;
        public SearchController()
        {
            flightBUS = new FlightBUS();
        }
        // GET: api/Search/{expression}
        [Route("api/Search/{expression}")]
        public IEnumerable<Flight> Get(string expression)
        {
            return flightBUS.Search(expression);
        }

        
    }
}
