namespace SmartKiwiTest;
using SmartKiwiApp.Models;
public class TimeoutTests
{
    private readonly int maxWaiteTime;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    private  QueueEngine queueEngine;
    private  List<ClientQueue> queueList;
    public TimeoutTests()
    {
        queueA = new ClientQueue("A", new Guid(), 3);
        queueB = new ClientQueue("B", new Guid(), 2);
        queueC = new ClientQueue("C", new Guid(), 2);
        queueList = new();
        maxWaiteTime = 10;
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
    }
    [Theory]
    [InlineData(true,false,false,"A_1")]
    [InlineData(false,true,false,"B_1")]
    [InlineData(false,false,true,"C_1")]
    public void Deve_Chamar_Fila_Em_TimeOut(bool aInTimeout, bool bInTimeout,bool cInTimeout, string expectedQueue)
    {
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        
        queueEngine.InicializeLastcallTime();
        queueA.Enqueue(new Client(Guid.NewGuid(), "TICKT", "A_1"));
        queueB.Enqueue(new Client(Guid.NewGuid(), "TICKT", "B_1"));
        queueC.Enqueue(new Client(Guid.NewGuid(), "TICKT", "C_1"));

        var timeLapsedTenMinutes = DateTime.Now.AddMinutes(-11);
        if (aInTimeout)
        {
            queueA.lastCallTime = timeLapsedTenMinutes;
        }

        if (bInTimeout)
        {
            queueB.lastCallTime = timeLapsedTenMinutes;
        }

        if (cInTimeout)
        {
            queueC.lastCallTime = timeLapsedTenMinutes;
        }
        

        var result = queueEngine.ProcessClient();
        Assert.NotNull(result);
        Assert.Equal(expectedQueue, result.Name);
        
    }

    [Theory]
    [InlineData(true,true,true,"A_1")]
    [InlineData(false,true,true, "B_1")]
    [InlineData(false,false,true, "C_1")]
    public void Deve_Chamar_Primera_Com_CLiente_Quanto_Todas_TimeOut(bool aHasClients, bool bHasClients,bool cHasClients, string expectedQueue)
    {
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        //queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine.InicializeLastcallTime();
        var timeLapsedTenMinutes = DateTime.Now.AddMinutes(-11);
        queueA.lastCallTime = timeLapsedTenMinutes;
        queueB.lastCallTime = timeLapsedTenMinutes;
        queueC.lastCallTime = timeLapsedTenMinutes;
        if (aHasClients)
        {
        queueA.Enqueue(new Client(Guid.NewGuid(), "TICKT", "A_1"));
        }
        if (bHasClients)
        {
        queueB.Enqueue(new Client(Guid.NewGuid(), "TICKT", "B_1"));
        }
        if (cHasClients)
        {
        queueC.Enqueue(new Client(Guid.NewGuid(), "TICKT", "C_1"));
        }
        
        var clientCalled = queueEngine.ProcessClient();
        Assert.NotNull(clientCalled);
        Assert.Equal(expectedQueue, clientCalled.Name);
        
    }
}