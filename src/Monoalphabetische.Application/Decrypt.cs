using System.Text;

namespace Monoalphabetische.Application;

public static class Decrypt
{
    public static void Process(Message message)
    {
        if (!message.IsValid)
        {
            throw new ArgumentException("Message is invalid. Maybe the encrypted/decrypted message contains an unsupported character or an unsupported key.");
        }

        if (string.IsNullOrWhiteSpace(message.EncryptedMessage))
        {
            throw new ArgumentException("Can't decrypt message because there is no encrypted message defined.");
        }

        if (message.Key == null)
        {
            throw new ArgumentException("Can't decrypt message because there is no key given.");
        }

        StringBuilder stringBuilder = new StringBuilder();
        foreach (char character in message.EncryptedMessage)
        {
            int index = getNewAlphabethIndex(character, Convert.ToInt32(message.Key));
            stringBuilder.Append(MessageHelper.Alphabeth[index]);
        }

        message.DecryptedMessage = stringBuilder.ToString();
    }

    private static int getNewAlphabethIndex(char character, int key)
    {
        int baseIndex = Array.IndexOf(MessageHelper.Alphabeth, character);
        int newIndex = baseIndex - key;

        if (newIndex < 0)
        {
            newIndex = MessageHelper.CharsetSize + newIndex;
        }

        return newIndex;
    }
}
