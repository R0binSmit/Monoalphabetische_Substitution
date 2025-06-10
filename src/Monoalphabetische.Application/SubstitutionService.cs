namespace Monoalphabetische.Application;

public class SubstitutionService
{
    public Message Encrypt(int key, string message)
    {
        var msg = new Message { Key = key, DecryptedMessagae = message };
        Encrypt.Process(msg);
        return msg;
    }

    public Message Decrypt(int key, string message)
    {
        var msg = new Message { Key = key, EncryptedMessage = message };
        Decrypt.Process(msg);
        return msg;
    }
}
