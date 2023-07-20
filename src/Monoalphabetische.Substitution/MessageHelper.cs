namespace Monoalphabetische.Substitution;

public static class MessageHelper
{
    public static readonly char[] Alphabeth = new char[] {
        'A', 'B', 'C', 'D', 'E', 'F', 'G',
        'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U',
        'V', 'W', 'X', 'Y', 'Z', '0', '1',
        '2', '3', '4', '5', '6', '7', '8',
        '9', ' ', '.', ','
    };

    public static bool IsValid(Message message)
    {
        if(_isMessageValid(message.EncryptedMessage) == false)
        {
            return false;
        }

        if(_isMessageValid(message.DecryptedMessagae) == false)
        {
            return false;
        }

        if(_isKeyValid(message.Key) == false)
        {
            return false;
        }

        return true;
    }

    private static bool _isMessageValid(string? message)
    {
        if(message == null) return true;

        bool isValid = true;
        foreach (char character in message)
        {
            if (Alphabeth.Contains(character) == false)
            {
                return false;
            }
        }

        return isValid;
    }

    private static bool _isKeyValid(int? key)
    {
        if (key == null) return true;
        return key >= Alphabeth.Length ? false : true;
    }
}
