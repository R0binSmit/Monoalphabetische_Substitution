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

        if(mode == "E")
        {
            EncryptMode(key, message);
        }
        else if(mode == "D") { }
        {
            DecryptMode(key, message);
        }

    }

    public static void EncryptMode(int key, string message)
    {
        Message messageObj = new Message() { Key = key, DecryptedMessagae = message };
        Encrypt._Encrypt(messageObj);
        Console.WriteLine($"Encrypted text: {messageObj.EncryptedMessage}");
    }

    public static void DecryptMode(int key, string message)
    {
        Message messageObj = new Message() { Key = key, EncryptedMessage= message };
        Decrypt._Decrypt(messageObj);
        Console.WriteLine(messageObj.DecryptedMessagae);
    }
}

