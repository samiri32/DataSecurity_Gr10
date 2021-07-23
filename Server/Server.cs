using System.Net.Security;
using System.Security.Cryptography;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace Server
{
    public class Server
    {
        private UdpClient udpClient;
        static RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();
        static DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
        public static string test;
        public Server()
        {
            udpClient = new UdpClient(12000);
            Task.Run(serverThread);
        }
      

        public void serverThread()
        {
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string base64 = Encoding.UTF8.GetString(receiveBytes);
                string mesazhi = base64;
                if (mesazhi != null)
                {
                    String msg = mesazhi;
                    udpClient.Send(Encoding.UTF8.GetBytes(msg), Encoding.UTF8.GetBytes(msg).Length, RemoteIpEndPoint);
                }

            }
        }
        

    }

}
