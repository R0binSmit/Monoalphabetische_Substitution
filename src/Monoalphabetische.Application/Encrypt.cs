using System.Text;

namespace Monoalphabetische.Application;

public static class Encrypt
{
    public static void Process(Message message)
    {
        if (!message.IsValid)
        {
            throw new ArgumentException("Message is invalid. Maybe the encrypted/decrypted message contains an unsupported character or an unsupported key.");
        }

        if(string.IsNullOrWhiteSpace(message.DecryptedMessage))
        {
            throw new ArgumentException("Can't encrypt message because there is no decrypted message defined.");
        }

        if(message.Key == null)
        {
            throw new ArgumentException("Can't encrypt message because there is no key given.");
        }

        StringBuilder stringBuilder = new StringBuilder();
        foreach (char character in message.DecryptedMessage)
        {
            int index = getNewAlphabethIndex(character, Convert.ToInt32(message.Key));
            stringBuilder.Append(MessageHelper.Alphabeth[index]);
        }

        message.EncryptedMessage = stringBuilder.ToString();
    }

    private static int getNewAlphabethIndex(char character, int key)
    {
        int baseIndex = character;
        int newIndex = baseIndex + key;

        if (newIndex >= MessageHelper.CharsetSize)
        {
            newIndex -= MessageHelper.CharsetSize;
        }

        return newIndex;
    }
}
