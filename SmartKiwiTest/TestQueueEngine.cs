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

    }

    [Fact]
    public void Deve_Adicionar_1_Nova_Fila_A_Lista_DE_FIlas()
    {
        queueEngine.AddQueue(queueA);
        var lenghtQueueList = queueEngine.QueueListLength();
        Assert.Equal(1,lenghtQueueList);
    
    }
    
    [Fact]
    public void Deve_Chamar_Fila_B_Se_Fila_A_Esvaziar()
    {
        queueB.Enqueue(new Client("Client_B",1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var clientCalLed = queueEngine.ProcessQueue();
        
        Assert.Equal("Client_B",clientCalLed.Name);
    
    }
}

