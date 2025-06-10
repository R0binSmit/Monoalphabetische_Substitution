using Monoalphabetische.Application;
using System.CommandLine;

namespace Monoalphabetische.Cli;

        var messageOption = new Option<string?>("--message", "Message to encrypt or decrypt");
        var keyOption = new Option<int?>("--key", () => null, "Key for the substitution");
        var modeOption = new Option<string?>("--mode", "E=Encrypt, D=Decrypt, G=Guess");
        var skipInputOption = new Option<bool>("--skip-input", "Skip interactive input");

        var rootCommand = new RootCommand
        {
            messageOption,
            keyOption,
            modeOption,
            skipInputOption
        };

        rootCommand.SetHandler((string? message, int? key, string? mode, bool skipInput) =>
        {
            var options = new CliOptions { Message = message, Key = key, Mode = mode, SkipInput = skipInput };
            Environment.ExitCode = RunWithOptions(options);
        }, messageOption, keyOption, modeOption, skipInputOption);

        return rootCommand.Invoke(args);
    public static int Main(string[] args)
    {
        return Parser.Default.ParseArguments<CliOptions>(args)
            .MapResult(RunWithOptions, _ => 1);
    }

    private static int RunWithOptions(CliOptions options)
    {
        var validator = new CliOptionsValidator();
        var result = validator.Validate(options);

        if (!options.SkipInput)
        {
            while (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                if (string.IsNullOrWhiteSpace(options.Message))
                {
                    Console.Write("Enter Message: ");
                    options.Message = Console.ReadLine();
                }
                if (string.IsNullOrWhiteSpace(options.Mode) || !(options.Mode == "E" || options.Mode == "D" || options.Mode == "G"))
                {
                    Console.Write("Select mode Encrypt (E), Decrypt (D) or Guess (G): ");
                    options.Mode = Console.ReadLine()?.ToUpperInvariant();
                }
                if ((options.Mode == "E" || options.Mode == "D") && options.Key == null)
                {
                    Console.Write("Enter Key: ");
                    if (int.TryParse(Console.ReadLine(), out var parsedKey))
                    {
                        options.Key = parsedKey;
                    }
                }

                result = validator.Validate(options);
            }
        }
        else if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return 1;
        }

        Execute(options);
        return 0;
    }

    private static void Execute(CliOptions options)
    {
        var service = new SubstitutionService();
        Message? result = null;

        Console.WriteLine($"Input text: {options.Message!.ToUpper()}");

        try
        {
            if (options.Mode == "E")
            {
                result = service.Encrypt(options.Key!.Value, options.Message!);
                Console.WriteLine($"Encrypted text: {result.EncryptedMessage}");
            }
            else if (options.Mode == "D")
            {
                result = service.Decrypt(options.Key!.Value, options.Message!);
                Console.WriteLine($"Decrypted text: {result.DecryptedMessage}");
            }
            else if (options.Mode == "G")
            {
                result = service.DecryptWithGuessedKey(options.Message!);
                Console.WriteLine($"Guessed Key: {result.Key}");
                Console.WriteLine($"Decrypted text: {result.DecryptedMessage}");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        if (options.Mode == "G" &&
            result != null &&
            !string.IsNullOrWhiteSpace(result.EncryptedMessage))
        {
            Analyse.AnalyseMessage(result.EncryptedMessage);
        }
    }

    private class CliOptions
    {
        public string? Message { get; set; }
        public int? Key { get; set; }
        public string? Mode { get; set; }
        public bool SkipInput { get; set; }
    }
}
