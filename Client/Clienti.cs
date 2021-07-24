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
        byte[] ClientKey;
        byte[] ServerInitialVector;

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
            var cert = new X509Certificate2(System.IO.File.ReadAllBytes("C:\\Users\\Meshira\\Desktop\\server.crt"));

            RSA rsa = (RSA)cert.PublicKey.Key;

            byte[] byteKey = rsa.Encrypt(ClientKey, RSAEncryptionPadding.Pkcs1);


            byte[] bytePlaintext = Encoding.UTF8.GetBytes(plaintext);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.Close();

            byte[] byteCiphertext = ms.ToArray();

            string iv = Convert.ToBase64String(ServerInitialVector);
            string ciphertxt = Convert.ToBase64String(byteCiphertext);
            string encryptedkey = Convert.ToBase64String(byteKey);

            return iv + "." + encryptedkey + "." + ciphertxt;

        }
        
         public string decrypt(string todecrypt)
        {
            
            string[] info = todecrypt.Split('.');
            ClientKey = Convert.FromBase64String(info[1]);
            ServerInitialVector = Convert.FromBase64String(info[0]);

            objDES.Key = ClientKey;
            objDES.IV = ServerInitialVector;

            objDES.Padding = PaddingMode.PKCS7;
            objDES.Mode = CipherMode.CBC;


            byte[] encryptedResponse = Convert.FromBase64String(info[2]);
            MemoryStream ms = new MemoryStream(encryptedResponse);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] decryptedResponse = new byte[ms.Length];
            cs.Read(decryptedResponse, 0, decryptedResponse.Length);
            cs.Close();

            string pergjigjaDekriptuar = Encoding.UTF8.GetString(decryptedResponse);
            return pergjigjaDekriptuar;
        }

        // public static void VerifyXml(XmlDocument xmlDoc, RSA key){
        //     CspParameters cspParams = new CspParameters();
        //     cspParams.KeyContainerName = "XML_DSIG_RSA_KEY";

        //     RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

        //     XmlDocument xmlDoc = new XmlDocument();

        //     xmlDoc.PreserveWhitespace = true;
        //     xmlDoc.Load("test.xml");

        //     Console.WriteLine("Verifying signature...");
        //     bool result = VerifyXml(xmlDoc, rsaKey);

        //     if (result)
        //     {
        //         Console.WriteLine("The XML signature is valid.");
        //     }
        //     else
        //     {
        //         Console.WriteLine("The XML signature is not valid.");
        //     }
        

        //   SignedXml signedXml = new SignedXml(xmlDoc);
        //   XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

        // if (nodeList.Count <= 0)
        // {
        //     throw new CryptographicException("Verification failed: No Signature was found in the document.");
        // }

        // if (nodeList.Count >= 2)
        // {
        //     throw new CryptographicException("Verification failed: More that one signature was found for the document.");
        // }

        // signedXml.LoadXml((XmlElement)nodeList[0]);


        // return signedXml.CheckSignature(key);
        // }
    }
}

