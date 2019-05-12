using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public interface IFlightBUS
    {
        string Ping();
        List<Flight> SelectAll();
        void Insert(Flight flight);
        void Update(Flight update);
        void Delete(int id);
        List<Flight> Search(string expression);
    }
}
