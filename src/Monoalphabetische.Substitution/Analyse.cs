namespace Monoalphabetische.Substitution;

public static class Analyse
{
    private static List<Letter> alphabeth = MessageHelper.Alphabeth
        .Select(c => new Letter(c)).ToList();

    private static void ResetCounters()
    {
        foreach(var letter in alphabeth)
        {
            letter.Counter = 0;
        }
    }

    private static void CountCharacters(string message)
    {
        foreach(char character in message)
        {
            Letter? letter = alphabeth.FirstOrDefault(l => l.Value == character);
            if(letter != null)
            {
                letter.Counter++;
            }
        }
    }

    public static void AnalyseMessage(string message)
    {
        if(message.Length < 30)
        {
            Console.WriteLine("Warning: Very short message - frequency analysis may be inaccurate.");
        }

        ResetCounters();
        CountCharacters(message);
        alphabeth = alphabeth.OrderByDescending(letter => letter.Counter).ToList();

        int indexMostCommonLetter = Array.IndexOf(MessageHelper.Alphabeth, alphabeth[0].Value);
        int indexLetterE = Array.IndexOf(MessageHelper.Alphabeth, 'E');
        int possibleKey = Math.Abs(indexMostCommonLetter - indexLetterE);
        Console.WriteLine($"Possible Key: { possibleKey }");
    }
}
