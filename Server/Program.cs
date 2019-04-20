using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using SharedLibrary;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpServerChannel serverChannel = new TcpServerChannel(2303);
                ChannelServices.RegisterChannel(serverChannel, true);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(BL.FlightBUS), "FlightBUS", WellKnownObjectMode.SingleCall);
                Console.WriteLine("Server is running");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to run server: "+ex.Message);
            }
            Console.Read();
        }
    }
}
