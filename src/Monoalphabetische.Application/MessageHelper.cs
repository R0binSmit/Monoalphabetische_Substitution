using System.Linq;
﻿namespace Monoalphabetische.Application;

public static class MessageHelper
{
    // Contains every UTF-16 code point so we can work with any
    // character representable in .NET strings.
    public static readonly char[] Alphabeth = Enumerable
        .Range(char.MinValue, char.MaxValue + 1)
        .Select(i => (char)i)
        .ToArray();

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
        // Every UTF-16 character is allowed, so any non-null string is valid.
        return message != null;
    }

    private static bool _isKeyValid(int? key)
    {
        if (key == null) return true;
        return key >= 0 && key < CharsetSize;
    }
}
