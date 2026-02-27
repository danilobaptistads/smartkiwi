namespace SmartKiwiTest;
using SmartKiwiApp.Models;

public class TestQueueEngine
{
    private readonly QueueEngine queueEngine;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    public TestQueueEngine()
    {
        queueEngine = new QueueEngine();
        queueA = new ClientQueue("Fila_A");
        queueB = new ClientQueue("Fila_B");
        queueC = new ClientQueue("Fila_C");
        queueA.SetPriority(3);
        queueB.SetPriority(2);
        queueC.SetPriority(1);
    }
   
    [Fact]
    public void Deve_Chamar_Fila_B_Se_A_For_Vazia()
    {
        queueB.Enqueue(new Client("Client_B",1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var clientCalLed = queueEngine.ProcessQueue();
        
        Assert.Equal("Client_B",clientCalLed.Name);
    
    }
        [Fact]
        public void Deve_Chamar_Fila_B_Se_CureentPriority_A_For_0()
    {   
        queueA.currentPriority = 0;
        queueA.Enqueue(new Client("Client_A_1",1));
        queueB.Enqueue(new Client("Client_B_1",1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var clientCalLed = queueEngine.ProcessQueue();
        
        Assert.Equal("Client_B_1",clientCalLed.Name);
    
    }
    [Fact]
    public void Deve_Retornar_Client_A_2()
    {
        queueA.Enqueue(new Client("Client_A_1",1));
        queueA.Enqueue(new Client("Client_A_2",1));
        queueB.Enqueue(new Client("Client_B",1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var clientCalLed = queueEngine.ProcessQueue();
        clientCalLed = queueEngine.ProcessQueue();
        clientCalLed = queueEngine.ProcessQueue();
        
        Assert.Equal("Client_A_2",clientCalLed.Name);
    
    }

}

