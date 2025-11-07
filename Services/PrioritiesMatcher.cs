
namespace SmartKiwi.Services;
using SmartKiwi.Models;
public class PrioritiesMatcher
{
    List<Queue> MainQueueList;
    List<Queue> DynamicQueueList;
    public PrioritiesMatcher(List<Queue> mainQueueList, List<Queue> dynamicQueueList)
    {
        MainQueueList= mainQueueList;
        DynamicQueueList = dynamicQueueList;
        
    }
    public bool check(bool hasPrioritieMatch)
    {

        if (hasPrioritieMatch != true)
        {

            for (var i = 1; i < MainQueueList.Count; i++)
            {

                if (MainQueueList[0].Priority == MainQueueList[i].currentPriority
                    && !DynamicQueueList.Contains(MainQueueList[i]))
                {

                    System.Console.WriteLine("Tem prioridade");
                    MainQueueList[i].currentPriority = 1;
                    DynamicQueueList.Add(MainQueueList[i]);
                    hasPrioritieMatch = true;
                }
            }
        }
        else
        {
            DynamicQueueList.Clear();
            hasPrioritieMatch = false;
        }
        if (hasPrioritieMatch == true)
        {
            DynamicQueueList[0].currentPriority = 1;
        }
        return hasPrioritieMatch;
    }
    
}





