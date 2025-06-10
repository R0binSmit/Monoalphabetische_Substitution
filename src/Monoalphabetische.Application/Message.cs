namespace Monoalphabetische.Application;

public class Message
{
    public string? EncryptedMessage { get; set; } = string.Empty;
    public string? DecryptedMessage { get; set; } = string.Empty;

    public int? Key { get; set; } = null;

    public bool IsValid => MessageHelper.IsValid(this);

}
