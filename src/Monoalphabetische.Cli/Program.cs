using FluentValidation;
using Monoalphabetische.Application;
using CommandLine;

namespace Monoalphabetische.Cli;

public class Program
{
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
        [Option("message", HelpText = "Message to encrypt or decrypt")]
        public string? Message { get; set; }

        [Option("key", HelpText = "Key for the substitution")]
        public int? Key { get; set; }

        [Option("mode", HelpText = "E=Encrypt, D=Decrypt, G=Guess")]
        public string? Mode { get; set; }

        [Option("skip-input", HelpText = "Skip interactive input")]
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
