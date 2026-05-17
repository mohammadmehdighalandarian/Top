namespace TopinLite.Services.Commons;

public interface IAesEncryption
{
    string Encrypt(string inputMessage, string key);
    string Decrypt(string encryptedHex, string key);
}