public class CycleChecker
{
    private int MaxWaite;
    private DateTime? LastAgingCheck;
    public CycleChecker(int maxWaite)
    {
        MaxWaite = maxWaite;
        LastAgingCheck = null;
    }
    public bool exec()
    {

        if (LastAgingCheck != null)
        {
            var TimeElapsed = DateTime.Now - LastAgingCheck;

            if (TimeElapsed >= TimeSpan.FromMinutes(MaxWaite))
            {
                System.Console.WriteLine("Passou 1 minuto");
                LastAgingCheck = DateTime.Now;
                return true;
            }
        }
        System.Console.WriteLine("não é um novo ciclo");
        LastAgingCheck = DateTime.Now;
        return false;
    } 
}


