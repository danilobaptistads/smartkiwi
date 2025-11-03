using System.Runtime.InteropServices.Marshalling;

public class CycleChecker
{
    private int MaxWaite;
    private DateTime? LastAgingCheck;
    private int i = 0;
    public CycleChecker(int maxWaite)
    {
        MaxWaite = maxWaite;
        LastAgingCheck = DateTime.Now;
        i = 0;
    }
    public bool exec()
    {
        i++;
        var TimeElapsed = DateTime.Now - LastAgingCheck;
        if (TimeElapsed >= TimeSpan.FromMinutes(MaxWaite))
        {
            Console.WriteLine($"Passou {i} segundos");
            LastAgingCheck = DateTime.Now;
            
            return true;
        }
 
        Console.WriteLine("não é um novo ciclo");
   
        return false;
        }
        
}


