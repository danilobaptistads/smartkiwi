namespace SmartKiwi.Models;
using System.Timers;
public class PriorityController
{
    public static void Aging(double maxAge)
    {
        var timer = new Timer(maxAge * 60 * 1000);
        timer.Elapsed += TimerElapsed;
        timer.AutoReset = true;
        timer.Start();

    }
    public static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        System.Console.WriteLine("teste timer");
    }

} 
