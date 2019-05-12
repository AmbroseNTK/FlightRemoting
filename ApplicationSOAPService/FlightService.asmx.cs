using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using SharedLibrary;
using Server;

namespace ApplicationSOAPService
{
    /// <summary>
    /// Summary description for FlightService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FlightService : System.Web.Services.WebService,IFlightBUS
    {

        Server.BL.FlightBUS flightBUS;

        private void Init()
        {
            if(flightBUS == null)
            {
                flightBUS = new Server.BL.FlightBUS();
            }
        }

        [WebMethod]
        public List<Flight> SelectAll()
        {
            Init();
            return flightBUS.SelectAll();
        }
        [WebMethod]
        public string Ping()
        {
            Init();
            return flightBUS.Ping();
        }
        [WebMethod]
        public void Insert(Flight flight)
        {
            Init();
            flightBUS.Insert(flight);
        }
        [WebMethod]
        public void Update(Flight update)
        {
            Init();
            flightBUS.Update(update);
        }
        [WebMethod]
        public void Delete(int id)
        {
            Init();
            flightBUS.Delete(id);
        }
        [WebMethod]
        public List<Flight> Search(string expression)
        {
            Init();
            return flightBUS.Search(expression);
        }
    }
}
