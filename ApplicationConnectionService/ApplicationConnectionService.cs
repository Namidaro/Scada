using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ULA.ApplicationConnectionService
{
    public class ApplicationConnectionService
    {

        public ApplicationConnectionService()
        {
            Connections = new List<TcpDeviceConnection>();
        }

        private static List<TcpDeviceConnection> Connections { get; set; }



        public TcpDeviceConnection CreateTcpDeviceConnection(string host, int port, int queryTimeout)
        {
            TcpDeviceConnection tcpDeviceConnection;
            if ((Connections.Count > 0) && (Connections.Exists(c => ((c.Host == host) && (c.Port == port)))))
            {
                tcpDeviceConnection = Connections.Find(c => ((c.Host == host) && (c.Port == port)));
                try
                {
                    tcpDeviceConnection.Dispose();
                }
                catch (Exception e)
                {

                }
                Connections.Remove(tcpDeviceConnection);
            }
            tcpDeviceConnection = new TcpDeviceConnection(host, port);
            if (queryTimeout != null) tcpDeviceConnection.SetTimeout(queryTimeout);
            Connections.Add(tcpDeviceConnection);
            return tcpDeviceConnection;
        }


    }
}