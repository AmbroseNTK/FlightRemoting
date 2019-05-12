using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FlightService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                RemotingConfiguration.CustomErrorsEnabled(true);
                TcpServerChannel serverChannel = new TcpServerChannel(2303);
                ChannelServices.RegisterChannel(serverChannel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(Server.BL.FlightBUS), "FlightBUS", WellKnownObjectMode.SingleCall);
                
            }
            catch (Exception ex)
            {
                
            }
            
        }

        protected override void OnStop()
        {
        }
    }
}
