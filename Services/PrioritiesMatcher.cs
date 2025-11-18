
namespace SmartKiwi.Services;
using SmartKiwi.Models;
public class PrioritiesMatcher
{
    List<Queue> MainQueueList;
    
    public PrioritiesMatcher(List<Queue> mainQueueList)
    {
        MainQueueList= mainQueueList;
    
        
    }
    public bool check(bool hasPrioritieMatch)
    {

        if (hasPrioritieMatch != true)
        {

            for (var i = 1; i < MainQueueList.Count; i++)
            {

                if (MainQueueList[0].Priority == MainQueueList[i].currentPriority)
                {

                    MainQueueList[i].currentPriority = 1;
        
                    hasPrioritieMatch = true;
                }
            }
        }
        else
        {

            hasPrioritieMatch = false;
        }
        if (hasPrioritieMatch == true)
        {
            MainQueueList[0].currentPriority = 1;
        }
        return hasPrioritieMatch;
    }
    
}





