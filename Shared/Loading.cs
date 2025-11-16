namespace Shared;

public static class Loading
{
    public static void Spinner(int duration)
    {
        string[] spinner = { "|", "/", "-", "\\" };

        Spin(spinner, duration);
    }

    public static void UnicodeSpinner(int duration)
    {
        string[] frames =
        {
            "⠋", "⠙", "⠹", "⠸",
            "⠼", "⠴", "⠦", "⠧",
            "⠇", "⠏"
        };

        Spin(frames, duration);
    }

    private static void Spin(string[]frames, int duration)
    {
        int index = 0;

        DateTime end = DateTime.Now.AddMilliseconds(duration);

        while (DateTime.Now < end)
        {
            Console.Write("\r\r" + frames[index] + " ");
            index = (index + 1) % frames.Length;
            Thread.Sleep(120);
        }

        Console.WriteLine("\rDone!");
    }
}