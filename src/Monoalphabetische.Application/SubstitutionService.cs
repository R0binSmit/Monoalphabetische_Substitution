namespace Monoalphabetische.Application;

public class SubstitutionService
{
    public Message Encrypt(int key, string message)
    {
        var msg = new Message { Key = key, DecryptedMessagae = message };
        Application.Encrypt.Process(msg);
        return msg;
    }

    public Message Decrypt(int key, string message)
    {
        var msg = new Message { Key = key, EncryptedMessage = message };
        Application.Decrypt.Process(msg);
        return msg;
    }
}
