namespace Monoalphabetische.Application;

public class SubstitutionService
{
    public Message Encrypt(int key, string message)
    {
        var msg = new Message { Key = key, DecryptedMessagae = message };
        Encrypt._Encrypt(msg);
        return msg;
    }

    public Message Decrypt(int key, string message)
    {
        var msg = new Message { Key = key, EncryptedMessage = message };
        Decrypt._Decrypt(msg);
        return msg;
    }
}
