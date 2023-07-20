using System.Security.Cryptography;

namespace Monoalphabetische.Substitution;

public class Program
{
    public static void Main(string[] args)
    {
        string? mode = null;
        string? message = null;
        Message? encryptMessage = null;
        int key = -1;

        while (message == string.Empty || message == null)
        {
            Console.Write("Enter Message: ");
            message = Console.ReadLine();
        }

        while (key == -1)
        {
            Console.Write("Enter Key: ");
            int.TryParse(Console.ReadLine(), out key);
        }

        while (mode != "E" && mode != "D")
        {
            Console.Write("Do you want do Encrypt (E) or Decrypt (D): ");
            mode = Console.ReadLine();
        }

        Console.WriteLine($"Input text: {message.ToUpper()}");

        if(mode == "E")
        {
            encryptMessage = EncryptMode(key, message);
        }
        
        if(mode == "D")
        {
            DecryptMode(key, message);
        }

        if(encryptMessage != null && string.IsNullOrWhiteSpace(encryptMessage.EncryptedMessage) == false)
        {
            Analyse.AnalyseMessage(encryptMessage.EncryptedMessage);
        }
    }

    public static Message? EncryptMode(int key, string message)
    {
        Message messageObj = new Message() { Key = key, DecryptedMessagae = message };

        try
        {
            Encrypt._Encrypt(messageObj);
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

        Console.WriteLine($"Encrypted text: {messageObj.EncryptedMessage}");
        return messageObj;
    }

    public static Message? DecryptMode(int key, string message)
    {
        Message messageObj = new Message() { Key = key, EncryptedMessage= message };

        try
        {
            Decrypt._Decrypt(messageObj);
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

        Console.WriteLine($"Decrypted text: {messageObj.DecryptedMessagae}");
        return messageObj;
    }
}

