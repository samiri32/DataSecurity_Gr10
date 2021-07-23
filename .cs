using System;

public class Client
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
            udpClient.Connect("localhost", 8000);
        }
        public void requestToServer()
        {
        }
        public void ClientSend(string plainText)
        {
            string message = Enkripto(plainText);
            udpClient.Send(Encoding.UTF8.GetBytes(message), Encoding.UTF8.GetBytes(message).Length);
        }
        public void merrCelesat()
        {
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var keyPath = Path.Combine(myDocs, "keys", "celesat.xml");
            StreamReader sr = new StreamReader(keyPath);
            string xmlParameters = sr.ReadToEnd();
            objRsa.FromXmlString(xmlParameters);
        }
        public string Enkripto(string plaintext)
        {
            String plainText = plaintext;
            byte[] ByteplainText = Encoding.UTF8.GetBytes(plainText);
            Console.WriteLine("Plain text : " + BitConverter.ToString(ByteplainText));
            objDes.GenerateIV();
            objDes.GenerateKey();
            InitialVector = objDes.IV;
            sharedKey = objDes.Key;
            objDes.Mode = CipherMode.CBC;
            objDes.Padding = PaddingMode.PKCS7;
            byte[] mesazhiEnkriptuar = objDes.CreateEncryptor().TransformFinalBlock(ByteplainText, 0, ByteplainText.Length);
            merrCelesat();
            byte[] encryptedKey = objRsa.Encrypt(sharedKey, true);
            Console.WriteLine("Celesi : " + BitConverter.ToString(sharedKey));
            StringBuilder sb = new StringBuilder();
            sb.Append(Convert.ToBase64String(InitialVector) + "*");
            sb.Append(Convert.ToBase64String(encryptedKey) + "*");
            sb.Append(Convert.ToBase64String(mesazhiEnkriptuar));
            return sb.ToString();
        }
        public string DekriptoPergjigjen()
        {
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            DESCryptoServiceProvider desKlient = new DESCryptoServiceProvider();
            String response = Encoding.UTF8.GetString(udpClient.Receive(ref remoteIPEndPoint));
            string[] arr = response.Split('*');
            byte[] initialV = Convert.FromBase64String(arr[0]);
            desKlient.IV = initialV;
            desKlient.Key = objDes.Key;
            byte[] encryptedResponse = Convert.FromBase64String(arr[1]);
            byte[] decryptedResponse;
            decryptedResponse = desKlient.CreateDecryptor().TransformFinalBlock(encryptedResponse, 0, encryptedResponse.Length);
            String pergjigjaDekriptuar = Encoding.UTF8.GetString(decryptedResponse);
            Console.WriteLine("Pergjgja Dekriptuar : " + pergjigjaDekriptuar);
            return pergjigjaDekriptuar;
        }


    }
}
}
