using System;

public class SecurityHelper
{
   
    public static string computeHash(string pwd) throws Exception
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
}
