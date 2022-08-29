namespace GP.Utilix;

public static class Randoms
{
    private static readonly Random Random = new Random();
    
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string RandomSentence(int wordsCount)
    {
        var sentence = "";
        for (int i = 1; i <= wordsCount; i++)
            sentence += RandomString(RandomInt(3, 8)) + " ";
        return sentence;
    }

    public static string RandomDescription()
    {
        string description = "";
        var linesCount = RandomInt(3, 8);
        for (int i = 0; i < linesCount; i++)
        {
            description += RandomSentence(RandomInt(10, 30)) + "\n";
        }

        return description;
    }

    public static string RandomEmail()
    {
        var email = RandomString(RandomInt(6, 15));
        email += $"@{RandomString(RandomInt(6, 8))}.com";
        return email;
    }

    public static string RandomNumericString(int length)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string RandomId()
    {
        return Guid.NewGuid().ToString();
    }

    public static int RandomInt(int min, int max)
    {
        return Random.Next(min, max);
    }
    
    public static int RandomInt(int max)
    {
        return Random.Next(max);
    }

    public static int RandomRate()
    {
        return Random.Next(1, 6);
    }

    public static int RandomQuantity()
    {
        return Random.Next(1, 4);
    }

    public static double RandomPrice()
    {
        return Random.Next(100, 1001);
    }

    public static int RandomDiscount()
    {
        var percents = new []{10, 15, 20, 25, 30, 40, 50, 60 ,70};
        var index = Random.Next(percents.Length);
        return percents[index];
    }

    public static string RandomPhoneNumber()
    {
        return $"09{RandomNumericString(8)}";
    }

    public static DateTime RandomDate()
    {
        var year = RandomInt(2000, 2022);
        var month = RandomInt(1, 12);
        
        var day = RandomInt(1, month == 2 ? 28 : 30);
        return new DateTime(year, month, day);
    }

    public static bool RandomBoolean()
    {
        const int threshHold = 3;
        return Randoms.RandomInt(1, 6) > threshHold;
    }
}