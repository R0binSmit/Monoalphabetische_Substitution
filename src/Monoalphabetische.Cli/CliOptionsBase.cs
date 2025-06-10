using CommandLine;

namespace Monoalphabetische.Cli
{
    public class CliOptionsBase
    {

        [Option("key", HelpText = "Key for the substitution")]
        public int? Key { get; set; }
        [Option("message", HelpText = "Message to encrypt or decrypt")]
        public string? Message { get; set; }

        [Option("mode", HelpText = "E=Encrypt, D=Decrypt, G=Guess")]
        public string? Mode { get; set; }

        [Option("skip-input", HelpText = "Skip interactive input")]
        public bool SkipInput { get; set; }
    }
}