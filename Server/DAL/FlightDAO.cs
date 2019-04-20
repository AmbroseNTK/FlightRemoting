using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using SharedLibrary;

namespace Server.DAL
{
    class FlightDAO
    {
        FlightsDBContext context = DBConnection.Instance.Context;
        public List<Flight> SelectAll() => context.Flight.ToList();

        public void Insert(Flight flight)
        {
            context.Flight.Add(flight);
            context.SaveChanges();
        }
        public void Update(Flight flight)
        {
            Flight updating = context.Flight.Where((f) => f.ID == flight.ID).Single();
            updating.Code = flight.Code;
            updating.ArrivalAirport = flight.ArrivalAirport;
            updating.ArrivalGate = flight.ArrivalGate;
            updating.DepatureAirport = flight.DepatureAirport;
            updating.DepatureGate = flight.DepatureGate;
            updating.Date = flight.Date;
            updating.CheckInTime = flight.CheckInTime;
            context.Update(updating);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Flight.Remove(context.Flight.Where((flight) => flight.ID == id).SingleOrDefault());
            context.SaveChanges();
        }
    }
}
