namespace SmartKiwi.Controller;

using SmartKiwi.Models;
using SmartKiwi.Services;

public class App
{
    public List<Queue> queueList = new List<Queue>();
    public QueueController queueController;
    public void Run()
    {
        var initialUi = new InitialUi();

        initialUi.WelcomeMessage();

        var queueBuilder = new QueueBuilder(queueList);
        var queueBuilderUi = new QueueBuilderUi(queueBuilder);

        if (queueList.Count() == 0)
        {
            initialUi.NotAnyQueueCreated();
            queueBuilderUi.Exec();
            var maxWaite = initialUi.GetMaxQueueWaite();
            queueController = new QueueController(queueList, maxWaite);
        }
        var checkInUi = new checkInUi(queueList);
        var attendantUi = new AttendantUi(queueController);
        var mainUi = new MainUi(queueBuilderUi, checkInUi, attendantUi);

        mainUi.Exec();
   

    }
    
    
}