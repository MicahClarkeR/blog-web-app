using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AESHelper
{
    private readonly byte[] Key;
    private readonly byte[] IV;

    private static AESHelper _instance = Create();

    public AESHelper(IConfiguration configuration)
    {
        string keyBase = configuration["EncryptionSettings:Key"];
        string ivBase64 = configuration["EncryptionSettings:IV"];

        if (string.IsNullOrEmpty(keyBase) || string.IsNullOrEmpty(ivBase64))
            throw new Exception("Encryption Key or IV is missing in configuration.");

        Key = Encoding.UTF8.GetBytes(keyBase); //Convert.FromBase64String(keyBase64);
    }

    public static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = _instance.Key;
            aes.IV = new byte[16];
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = _instance.Key;
            aes.IV = new byte[16];
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

    private static AESHelper Create()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables() // Allows reading from env vars
            .Build();

        return new AESHelper(configuration);
    }
}
