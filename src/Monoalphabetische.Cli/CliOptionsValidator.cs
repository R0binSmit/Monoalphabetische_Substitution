using FluentValidation;

namespace Monoalphabetische.Cli;

public partial class Program
{
    private sealed class CliOptionsValidator : AbstractValidator<CliOptions>
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
