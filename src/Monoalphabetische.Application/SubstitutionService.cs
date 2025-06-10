namespace Monoalphabetische.Application;

public class SubstitutionService
{
    public Message Encrypt(int key, string message)
    {
        var msg = new Message { Key = key, DecryptedMessage = message.ToUpper() };
        Application.Encrypt.Process(msg);
        return msg;
    }

    public Message Decrypt(int key, string message)
    {
        var msg = new Message { Key = key, EncryptedMessage = message.ToUpper() };
        Application.Decrypt.Process(msg);
        return msg;
    }

    public Message DecryptWithGuessedKey(string message)
    {
        int key = Analyse.GuessKey(message);
        var msg = new Message { Key = key, EncryptedMessage = message.ToUpper() };
        Application.Decrypt.Process(msg);
        return msg;
    }
}
