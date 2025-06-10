using FluentValidation;
using Monoalphabetische.Application;
using System.CommandLine;

namespace Monoalphabetische.Cli;

public class Program
{
    public static int Main(string[] args)
    {
        var messageOption = new Option<string?>("--message", "Message to encrypt or decrypt");
        var keyOption = new Option<int?>("--key", () => null, "Key for the substitution");
        var modeOption = new Option<string?>("--mode", "E=Encrypt, D=Decrypt, G=Guess");
        var skipInputOption = new Option<bool>("--skip-input", "Skip interactive input");

        var root = new RootCommand("Monoalphabetic Substitution");
        root.AddOption(messageOption);
        root.AddOption(keyOption);
        root.AddOption(modeOption);
        root.AddOption(skipInputOption);

        root.SetHandler((string? message, int? key, string? mode, bool skipInput) =>
        {
            var options = new CliOptions
            {
                Message = message,
                Key = key,
                Mode = mode,
                SkipInput = skipInput
            };
            var validator = new CliOptionsValidator();
            var result = validator.Validate(options);

            if (!skipInput)
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
                return;
            }

            Execute(options);
        }, messageOption, keyOption, modeOption, skipInputOption);

        return root.Invoke(args);
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
                Console.WriteLine($"Decrypted text: {result.DecryptedMessagae}");
            }
            else if (options.Mode == "G")
            {
                result = service.DecryptWithGuessedKey(options.Message!);
                Console.WriteLine($"Guessed Key: {result.Key}");
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

    private class CliOptions
    {
        public string? Message { get; set; }
        public int? Key { get; set; }
        public string? Mode { get; set; }
        public bool SkipInput { get; set; }
    }

    private class CliOptionsValidator : AbstractValidator<CliOptions>
    {
        public CliOptionsValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.");

            RuleFor(x => x.Mode)
                .Must(m => m == "E" || m == "D" || m == "G")
                .WithMessage("Mode must be E, D or G.");

            When(x => x.Mode == "E" || x.Mode == "D", () =>
            {
                RuleFor(x => x.Key).NotNull().WithMessage("Key is required when mode is E or D.");
            });
        }
    }
}
