using System.Text;

namespace Monoalphabetische.Application;

public static class Encrypt
{
    public static void _Encrypt(Message message)
    {
        if (!message.IsValid)
        {
            throw new ArgumentException("Message is invalid. Maybe the encrypted/decrypted message contains an unsupported character or an unsupported key.");
        }

        if(string.IsNullOrWhiteSpace(message.DecryptedMessagae))
        {
            throw new ArgumentException("Can't encrypt message because there is no decrypted message defined.");
        }

        if(message.Key == null)
        {
            throw new ArgumentException("Can't encrypt message because there is not key given.");
        }

        StringBuilder stringBuilder = new StringBuilder();
        foreach (char character in message.DecryptedMessagae) 
        {
            int index = getNewAlphabethIndex(character, Convert.ToInt32(message.Key));
            stringBuilder.Append(MessageHelper.Alphabeth[index]);
        }

        message.EncryptedMessage = stringBuilder.ToString();
    }

    private static int getNewAlphabethIndex(char character, int key)
    {
        int baseIndex = Array.IndexOf(MessageHelper.Alphabeth, character);
        int newIndex = baseIndex + key;

        if (newIndex >= MessageHelper.Alphabeth.Length)
        {
            newIndex -= MessageHelper.Alphabeth.Length;
        }

        return newIndex;
    }
}
