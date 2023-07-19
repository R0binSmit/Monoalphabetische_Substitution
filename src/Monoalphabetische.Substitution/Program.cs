namespace Monoalphabetische.Substitution;

public class Program
{
    public static void Main(string[] args)
    {
        string? mode = null;
        string? message = null;
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
            EncryptMode(key, message);
        }
        
        if(mode == "D")
        {
            DecryptMode(key, message);
        }

    }

    public static void EncryptMode(int key, string message)
    {
        Message messageObj = new Message() { Key = key, DecryptedMessagae = message };

        try
        {
            Encrypt._Encrypt(messageObj);
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        Console.WriteLine($"Encrypted text: {messageObj.EncryptedMessage}");
    }

    public static void DecryptMode(int key, string message)
    {
        Message messageObj = new Message() { Key = key, EncryptedMessage= message };

        try
        {
            Decrypt._Decrypt(messageObj);
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        Console.WriteLine($"Decrypted text: {messageObj.DecryptedMessagae}");
    }
}

