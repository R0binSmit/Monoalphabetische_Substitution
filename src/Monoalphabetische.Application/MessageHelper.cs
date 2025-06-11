using System.Globalization;
using System.Linq;

namespace Monoalphabetische.Application;

public static class MessageHelper
{
    // Contains all printable characters so encryption/decryption never
    // produces control codes or surrogates.
    public static readonly char[] Alphabeth = Enumerable
        .Range(char.MinValue, char.MaxValue + 1)
        .Select(i => (char)i)
        .Where(IsPrintable)
        .ToArray();

    private static bool IsPrintable(char c)
    {
        UnicodeCategory category = char.GetUnicodeCategory(c);
        return category != UnicodeCategory.Control &&
               category != UnicodeCategory.Format &&
               category != UnicodeCategory.PrivateUse &&
               category != UnicodeCategory.Surrogate &&
               category != UnicodeCategory.OtherNotAssigned &&
               category != UnicodeCategory.LineSeparator &&
               category != UnicodeCategory.ParagraphSeparator;
    }

    public static int CharsetSize => Alphabeth.Length;

    public static bool IsValid(Message message)
    {
        if (!_isMessageValid(message.EncryptedMessage))
        {
            return false;
        }

        if (!_isMessageValid(message.DecryptedMessage))
        {
            return false;
        }

        if (!_isKeyValid(message.Key))
        {
            return false;
        }

        return true;
    }

    private static bool _isMessageValid(string? message)
    {
        if (message == null) return true;

        foreach (char c in message)
        {
            if (!IsPrintable(c))
            {
                return false;
            }
        }

        return true;
    }

    private static bool _isKeyValid(int? key)
    {
        if (key == null) return true;
        return key >= 0 && key < CharsetSize;
    }
}
