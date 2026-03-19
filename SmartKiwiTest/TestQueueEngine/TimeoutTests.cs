namespace SmartKiwiTest;
using SmartKiwiApp.Models;
public class TimeoutTests
{
    private readonly QueueEngine queueEngine;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    public TimeoutTests()
    {
        queueEngine = new QueueEngine(10);
        queueA = new ClientQueue("A");
        queueB = new ClientQueue("B");
        queueC = new ClientQueue("C");
        queueA.SetPriority(3);
        queueB.SetPriority(2);
        queueC.SetPriority(1);
    }
    [Theory]
    [InlineData(true,false,false,"A_1")]
    [InlineData(false,true,false,"B_1")]
    [InlineData(false,false,true,"C_1")]
    public void Deve_Chamar_Fila_Em_TimeOut(bool aInTimeout, bool bInTimeout,bool cInTimeout, string expectedQueue)
    {
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);
        queueEngine.AddQueue(queueC);
        queueEngine.InicializeLastcallTime();
        queueA.Enqueue(new Client("A_1", 1));
        queueB.Enqueue(new Client("B_1", 1));
        queueC.Enqueue(new Client("C_1", 1));

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

        Assert.Equal(expectedQueue, result.Name);
        
    }

    [Theory]
    [InlineData(true,true,true,"A_1")]
    [InlineData(false,true,true, "B_1")]
    [InlineData(false,false,true, "C_1")]
    public void Deve_Chamar_Primera_Com_CLiente_Quanto_Todas_TimeOut(bool aHasClients, bool bHasClients,bool cHasClients, string expectedQueue)
    {
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);
        queueEngine.AddQueue(queueC);
        queueEngine.InicializeLastcallTime();
        var timeLapsedTenMinutes = DateTime.Now.AddMinutes(-11);
        queueA.lastCallTime = timeLapsedTenMinutes;
        queueB.lastCallTime = timeLapsedTenMinutes;
        queueC.lastCallTime = timeLapsedTenMinutes;
        if (aHasClients)
        {
        queueA.Enqueue(new Client("A_1", 1));
        }
        if (bHasClients)
        {
        queueB.Enqueue(new Client("B_1", 1));
        }
        if (cHasClients)
        {
        queueC.Enqueue(new Client("C_1", 1));
        }
        
        var clientCalled = queueEngine.ProcessClient();
        Assert.Equal(expectedQueue, clientCalled.Name);
        
    }
}