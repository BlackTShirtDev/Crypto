//Logan Marble
//CIS 405
//Assingment 2 Part A

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace RSAEncryptDecrypt
{
  class RSAEncryptDecrypt
  {
    static string CIPHERTEXT = "MarbleCipherText.txt";
    static string RECOVEREDMESSAGE = "MarbleCipherText.txt";

    static void Main(string[] args)
    {
      Encrypt();
      Decrypt();
    }

    static void Encrypt()
    {
      //Student encypt their SS number
      byte[] plainText = Encoding.UTF8.GetBytes("S00586492");

      RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

      // get public RSA key 
      StreamReader reader = new StreamReader("CollinsPublicOnlyKey.xml");
      string RSAPublicOnlyKeyXML = reader.ReadToEnd();
      rsa.FromXmlString(RSAPublicOnlyKeyXML);
      reader.Close();

      // encrypt using RSA public key
      byte[] cipherbytes = rsa.Encrypt(plainText, true);

      FileStream fout = File.Create(CIPHERTEXT);


      fout.Write(cipherbytes, 0, cipherbytes.Length);
      fout.Close();
    }

    static void Decrypt()
    {
      RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

      //read public and privateRSA parameters for encrypt

      //read encrypted file and store in byte array  
      FileStream fin = File.OpenRead(CIPHERTEXT);
      byte[] cipherBytes = new byte[fin.Length];
      fin.Read(cipherBytes, 0, cipherBytes.Length);
      fin.Close();

      //decrypt byte array using OAEP padding
      RSACryptoServiceProvider rsaDEC = new RSACryptoServiceProvider();

      StreamReader reader = new StreamReader("CollinsPublicPrivateKey.xml");
      string RSAPublicPrivateKeyXML = reader.ReadToEnd();
      rsaDEC.FromXmlString(RSAPublicPrivateKeyXML);
      reader.Close();

      // decrypt cipherbytes and store into byte array newPlainText
      byte[] newPlainText = rsaDEC.Decrypt(cipherBytes, true);

      Console.WriteLine(Encoding.UTF8.GetString(newPlainText));
      Console.WriteLine("Hit any key to continue ");
      Console.ReadLine();
    }
  }
}



