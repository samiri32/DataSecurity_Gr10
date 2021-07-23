using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public class Client
    {
        static RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();
        static DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
        byte[] InitialVector;
        byte[] sharedKey;
        public UdpClient udpClient;
        public Client()
        {
            udpClient = new UdpClient();
            udpClient.Connect("localhost", 12000);
            ClientSend("O SAMIR O OEWQRJKLQWJHLTJHLWQTLKJH:LKWQJT");
            DekriptoPergjigjen();
        }
        public void requestToServer()
        {
        }
        public void ClientSend(string plainText)
        {
            string message = plainText;
            udpClient.Send(Encoding.UTF8.GetBytes(message), Encoding.UTF8.GetBytes(message).Length);
        }

        public void DekriptoPergjigjen()
        {
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            DESCryptoServiceProvider desKlient = new DESCryptoServiceProvider();
            String response = Encoding.UTF8.GetString(udpClient.Receive(ref remoteIPEndPoint));
            MessageBox.Show(response);
            //string[] arr = response.Split('*');
            //byte[] initialV = Convert.FromBase64String(arr[0]);
            //desKlient.IV = initialV;
           // desKlient.Key = objDes.Key;
           // byte[] encryptedResponse = Convert.FromBase64String(arr[1]);
            //byte[] decryptedResponse;
            //decryptedResponse = desKlient.CreateDecryptor().TransformFinalBlock(encryptedResponse, 0, encryptedResponse.Length);
            //String pergjigjaDekriptuar = Encoding.UTF8.GetString(decryptedResponse);
           // Console.WriteLine("Pergjgja Dekriptuar : " + pergjigjaDekriptuar);
            //return pergjigjaDekriptuar;
        }

    }
}

