using System.Security.Cryptography;
using System.Text;
using TopinLite.Services.Commons;

namespace TopinLite.Infra.Common.Services;

public class AesEncryptionService : IAesEncryption
{
    private static readonly byte[] IV = Encoding.UTF8
                                                 .GetBytes("87965412369587642839571358935988")
                                                 .Take(16)
                                                 .ToArray();

    public string Encrypt(string inputMessage, string key)
    {
        if (string.IsNullOrEmpty(inputMessage))
            throw new ArgumentNullException(nameof(inputMessage));
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        byte[] keyBytes = PrepareKey(key);
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputMessage);

        using var aes = CreateAes(keyBytes);
        using var encryptor = aes.CreateEncryptor();
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        cs.Write(inputBytes, 0, inputBytes.Length);
        cs.FlushFinalBlock();

        return Convert.ToHexString(ms.ToArray());
    }


    public string Decrypt(string encryptedHex, string key)
    {
        if (string.IsNullOrEmpty(encryptedHex))
            throw new ArgumentNullException(nameof(encryptedHex));
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        byte[] keyBytes = PrepareKey(key);
        byte[] encryptedBytes = Convert.FromHexString(encryptedHex);

        using var aes = CreateAes(keyBytes);
        using var decryptor = aes.CreateDecryptor();
        using var ms = new MemoryStream(encryptedBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs, Encoding.UTF8);

        return reader.ReadToEnd();
    }

    private static byte[] PrepareKey(string key)
    {
        if (key.Length > 31)
            key = key[^32..];          

        key = key.PadRight(32, '0');    

        return Encoding.UTF8.GetBytes(key);
    }

    private static Aes CreateAes(byte[] keyBytes)
    {
        var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;       
        aes.Padding = PaddingMode.PKCS7;    
        aes.KeySize = 256;                  
        aes.Key = keyBytes;
        aes.IV = IV;
        return aes;
    }
}