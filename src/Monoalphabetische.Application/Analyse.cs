namespace Monoalphabetische.Application;

public static class Analyse
{
    // Counter for each printable character defined in MessageHelper
    private static readonly int[] counters = new int[MessageHelper.CharsetSize];

    private static void ResetCounters()
    {
        Array.Clear(counters, 0, counters.Length);
    }

    private static void CountCharacters(string message)
    {
        foreach(char character in message)
        {
            int index = Array.IndexOf(MessageHelper.Alphabeth, character);
            if (index >= 0)
            {
                counters[index]++;
            }
        }
    }

    public static int GuessKey(string message)
    {
        if(message.Length < 30)
        {
            Console.WriteLine("Warning: Very short message - frequency analysis may be inaccurate.");
        }

        ResetCounters();
        CountCharacters(message);

        int indexMostCommonLetter = 0;
        for (int i = 1; i < counters.Length; i++)
        {
            if (counters[i] > counters[indexMostCommonLetter])
            {
                indexMostCommonLetter = i;
            }
        }

        int indexLetterE = Array.IndexOf(MessageHelper.Alphabeth, 'E');
        int possibleKey = Math.Abs(indexMostCommonLetter - indexLetterE);
        return possibleKey;
    }

    public static void AnalyseMessage(string message)
    {
        int possibleKey = GuessKey(message);
        Console.WriteLine($"Possible Key: { possibleKey }");
    }
}
