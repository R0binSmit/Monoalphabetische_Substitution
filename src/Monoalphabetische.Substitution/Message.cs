namespace Monoalphabetische.Substitution;

public class Message
{
    private string? _encryptedMessage = null;
    private string? _decryptedMessage = null;

    public string? EncryptedMessage
    {
        get { return _encryptedMessage; }
        set
        {
            if (value != null)
            {
                _encryptedMessage = value.ToUpper();
            }
        }
    }
    public string? DecryptedMessagae {
        get { return _decryptedMessage; }
        set 
        {
            if (value != null)
            {
                _decryptedMessage = value.ToUpper();
            }
        }
    }
    public int? Key { get; set; } = null;

    public bool IsValid => MessageHelper.IsValid(this);

}
