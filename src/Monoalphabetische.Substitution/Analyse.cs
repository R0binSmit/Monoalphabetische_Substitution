namespace Monoalphabetische.Substitution;

public static class Analyse
{
    private static List<Letter> alphabeth = new List<Letter>()
    {
        new Letter('A'), new Letter('B'), new Letter('C'),
        new Letter('D'), new Letter('E'), new Letter('F'),
        new Letter('G'), new Letter('H'), new Letter('I'),
        new Letter('J'), new Letter('K'), new Letter('L'),
        new Letter('M'), new Letter('N'), new Letter('O'),
        new Letter('P'), new Letter('Q'), new Letter('R'),
        new Letter('S'), new Letter('T'), new Letter('U'),
        new Letter('V'), new Letter('W'), new Letter('X'),
        new Letter('Y'), new Letter('Z'), new Letter(' '),
        new Letter(','), new Letter('.')
    };

    private static void CountCharacters(string message)
    {
        foreach(char character in message)
        {
            Letter? letter = alphabeth.Where(letter => letter.Value == character).FirstOrDefault();

            if(letter != null)
            {
                letter.Counter++;
            }
        }
    }

    public static void AnalyseMessage(string message)
    {
        CountCharacters(message);
        alphabeth = alphabeth.OrderByDescending(letter => letter.Counter).ToList();

        int indexMostCommonLetter = Array.IndexOf(MessageHelper.Alphabeth, alphabeth[0].Value);
        int indexLetterE = Array.IndexOf(MessageHelper.Alphabeth, 'E');

        if(indexMostCommonLetter > indexLetterE)
        {
            Console.WriteLine($"Possible Key: { indexMostCommonLetter - indexLetterE }");
        }
        else if(indexLetterE > indexMostCommonLetter)
        {
            Console.WriteLine($"Possible Key: { indexLetterE - indexMostCommonLetter }");
        }
    }
}
