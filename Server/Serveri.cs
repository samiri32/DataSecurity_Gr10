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
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace Server
{
    public class Serveri
    {
        private UdpClient udpClient;
        X509Certificate2 certifikata;
        RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        byte[] ServerKey;
        byte[] ServerInitialVector;
        public static string test;
        JsonDB db = new JsonDB("C:\\Users\\Asus\\source\\repos\\Siguria_2021_Gr10-Detyra2\\Server\\users.json");
        public Serveri()
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
                string mesazhi = decrypt(base64);
                if (mesazhi != null)
                {
                   
                        string[] arr = mesazhi.Split('.');
                    switch (arr[2])
                    {
                        case "1":
                            if (arr.Length == 3)
                            {
                                string username = arr[0];
                                string password = arr[1];

                            }

                            break;

                        default:
                            if (arr.Length == 4)
                            {
                                string fullname = arr[0];
                                string username = arr[1];
                                string password = arr[2];
                                db.AddUser(new User(fullname, username, password));
                            }
                            break;
                    
                        }
                    
                    
                    // String msg = "Serveri: simnica";
                    // udpClient.Send(Encoding.UTF8.GetBytes(msg), Encoding.UTF8.GetBytes(msg).Length, RemoteIpEndPoint);
                }

            }
        }
        private string decrypt(string ciphertext)
        {

            string[] info = ciphertext.Split('.');
            ServerKey = Convert.FromBase64String(info[1]);
            ServerInitialVector = Convert.FromBase64String(info[0]);

            objDES.IV = ServerInitialVector;

            objDES.Padding = PaddingMode.PKCS7;
            objDES.Mode = CipherMode.CBC;

            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var keyPath = Path.Combine(myDocs, "server.xml");
            StreamReader sr = new StreamReader(keyPath);
            string xmlParameters = sr.ReadToEnd();
            objRSA.FromXmlString(xmlParameters);

            
            byte[] byteKey = objRSA.Decrypt(Convert.FromBase64String(info[1]), RSAEncryptionPadding.Pkcs1);

            objDES.Key = byteKey;
            

            

            byte[] byteCiphertexti = Convert.FromBase64String(info[2]);
            MemoryStream ms = new MemoryStream(byteCiphertexti);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] byteTextiDekriptuar = new byte[ms.Length];
            cs.Read(byteTextiDekriptuar, 0, byteTextiDekriptuar.Length);
            cs.Close();

            string decryptedText = Encoding.UTF8.GetString(byteTextiDekriptuar);
            return decryptedText;

        }


    }

}
