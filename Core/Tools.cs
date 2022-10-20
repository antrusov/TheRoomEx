using static System.Console;

class Tools
{
    public static void Alert (string msg)
    {
        var old = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkRed;

        WriteLine(msg);

        ForegroundColor = old;
    }

    public static int GetInt (string message, int min, int max)
    {
        int result = min;
        bool valid = false;
        do
        {
            Write(message);
            valid = int.TryParse(ReadLine(), out result);
        } while (!valid || result < min || result > max);
        return result;
    }
}