using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public class Clienti
    {
        X509Certificate2 certifikata = new X509Certificate2();
        RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        Socket klienti;
        byte[] ClientKey;
        byte[] ServerInitialVector;

        
        byte[] InitialVector;
        byte[] sharedKey;
        public UdpClient udpClient;
        public Clienti()
        {
            udpClient = new UdpClient();
            udpClient.Connect("localhost", 12000);
            
        }
        public void requestToServer()
        {
        }
        public string encrypt(string plaintext)
        {
            objDES.GenerateKey();
            objDES.GenerateIV();
            ClientKey = objDES.Key;
            ServerInitialVector = objDES.IV;

            objDES.Mode = CipherMode.CBC;
            objDES.Padding = PaddingMode.PKCS7;           
            var cert = new X509Certificate2(System.IO.File.ReadAllBytes("C:\\Users\\Asus\\Desktop\\server.crt"));

            RSA rsa = (RSA)cert.PublicKey.Key;

            byte[] byteKey = rsa.Encrypt(ClientKey, RSAEncryptionPadding.Pkcs1);


            byte[] bytePlaintext = Encoding.UTF8.GetBytes(plaintext);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.Close();

            byte[] byteCiphertext = ms.ToArray();

            string iv = Convert.ToBase64String(ServerInitialVector);
            string key = Convert.ToBase64String(ClientKey);
            string ciphertxt = Convert.ToBase64String(byteCiphertext);
            string encryptedkey = Convert.ToBase64String(byteKey);

            return iv + "." + encryptedkey + "." + ciphertxt;
           


        }
        /*public string DekriptoPergjigjen(string ciphertext)
        {
            string[] info = ciphertext.Split();

            ClientKey = Convert.FromBase64String(info[1]);
            ClientIV = Convert.FromBase64String(info[0]);

            objDES.IV = ClientIV;

            objDES.Padding = PaddingMode.PKCS7;
            objDES.Mode = CipherMode.CBC;

            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var keyPath = Path.Combine(myDocs, "arbresha", "server.xml");
            StreamReader sr = new StreamReader(keyPath);
            string xmlParams = sr.ReadToEnd();
            objRSA.FromXmlString(xmlParams);

            byte[] byteKey = objRSA.Decrypt(Convert.FromBase64String(info[1]), RSAEncryptionPadding.Pkcs1);
            objDES.Key = byteKey;
            byte[] byteCipherText = Convert.FromBase64String(info[2]);
            MemoryStream ms = new MemoryStream(byteCipherText);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] byteTextiDekriptuar = new byte[ms.Length];
            cs.Read(byteTextiDekriptuar, 0, byteTextiDekriptuar.Length);
            cs.Close();

            string decryptedText = Encoding.UTF8.GetString(byteTextiDekriptuar);
            return decryptedText;
        }*/

    }
}

