
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
                if (MainQueueList[i].IsEmpty() && DynamicQueueList.Contains(MainQueueList[i]))
                {
                    DynamicQueueList.Remove(MainQueueList[i]);
                    continue;
                }

                if (MainQueueList[0].currentPriority == MainQueueList[i].currentPriority)
                {
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
        return hasPrioritieMatch;
    }
}




