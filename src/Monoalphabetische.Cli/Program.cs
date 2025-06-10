using Monoalphabetische.Application;

namespace Monoalphabetische.Cli;

public class Program
{
    public static void Main(string[] args)
    {
        string? mode = null;
        string? message = null;
        int key = -1;
        var service = new SubstitutionService();
        Message? result = null;

        while (string.IsNullOrWhiteSpace(message))
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
            Console.Write("Do you want to Encrypt (E) or Decrypt (D): ");
            mode = Console.ReadLine();
        }

        Console.WriteLine($"Input text: {message.ToUpper()}");

        try
        {
            if (mode == "E")
            {
                result = service.Encrypt(key, message);
                Console.WriteLine($"Encrypted text: {result.EncryptedMessage}");
            }
            else if (mode == "D")
            {
                result = service.Decrypt(key, message);
                Console.WriteLine($"Decrypted text: {result.DecryptedMessagae}");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        if (result != null && !string.IsNullOrWhiteSpace(result.EncryptedMessage))
        {
            Analyse.AnalyseMessage(result.EncryptedMessage);
        }
    }
}
