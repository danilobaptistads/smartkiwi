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
  
    }
    public bool exec()
    {

        var TimeElapsed = DateTime.Now - LastAgingCheck;
        if (TimeElapsed >= TimeSpan.FromMinutes(MaxWaite))
        {
  
            LastAgingCheck = DateTime.Now;
            
            return true;
        }
    
        return false;
        }
        
}


