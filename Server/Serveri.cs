using System.Security.Cryptography;
using System.IO;
using System;
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
        RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        byte[] ServerKey;
        byte[] ServerInitialVector;
        JsonDB db = new JsonDB("C:\\Users\\Meshira\\desktop\\Siguria_2021_Gr10-Detyra2\\Server\\users.json");
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
                Funksioni(mesazhi);
                //SignXml();
                string pergjigja = encrypt("Serveri pergjigjet ketu.");
                udpClient.Send(Encoding.UTF8.GetBytes(pergjigja), Encoding.UTF8.GetBytes(pergjigja).Length, RemoteIpEndPoint);
            }

        }
        

        private void Funksioni(string mess){
             if (mess != null)
                {
                   
                        string[] arr = mess.Split('.');
                    switch (arr[2])
                    {
                        case "1":
                            if (arr.Length == 3)
                            {
                                string username1 = arr[0];
                                var userigjetur = db.FindByUsername(username1);
                                //validimi
                                //hapja e formes se re 
                                //ok ose error msg
                            }

                            break;

                        default:
                            if (arr.Length == 4)
                            {
                                string fullname = arr[0];
                                string username = arr[1];
                                string hashedpasswordi = computeHash(arr[2]);
                                db.AddUser(new User(fullname, username, hashedpasswordi));
                            }
                            break;
                    
                        }
                }
        }

        public static string computeHash(string pwd)
      {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        var pbkdf2 = new Rfc2898DeriveBytes(pwd, salt, 10000);

        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
 
        string hashedpsw = Convert.ToBase64String(hashBytes);
        return hashedpsw;
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

        private string encrypt(string plaintext)
        {
            objDES.GenerateIV();
            objDES.GenerateKey();
            ServerKey = objDES.Key;
            ServerInitialVector = objDES.IV;

            objDES.Padding = PaddingMode.PKCS7;
            objDES.Mode = CipherMode.CBC;
            
            byte[] plainText = Encoding.UTF8.GetBytes(plaintext);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            
            cs.Write(plainText, 0, plainText.Length);
            cs.Close();

            
            byte[] mesazhiEnkriptuar = ms.ToArray();
            string ivprim = Convert.ToBase64String(ServerInitialVector);
            string key = Convert.ToBase64String(ServerKey);
            string ciphertxt = Convert.ToBase64String(mesazhiEnkriptuar);
            return ivprim + "." + key + "." + ciphertxt;
        }


    

    public static void SignXml(){
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = "XML_DSIG_RSA_KEY";
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load("test.xml");


            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = rsaKey;

            Reference reference = new Reference();
            reference.Uri = "";
 
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            signedXml.AddReference(reference);
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            Console.WriteLine("XML file signed.");
            xmlDoc.Save("test.xml");
    }

    }

}
