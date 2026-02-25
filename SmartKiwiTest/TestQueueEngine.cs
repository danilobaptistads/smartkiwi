namespace SmartKiwiTest;
using SmartKiwiApp.Models;

public class TestQueueEngine
{
    [Fact]
    public void Deve_Adicionar_1_Nova_Fila_A_Lista_DE_FIlas()
    {
        var newQueue = new ClientQueue("TEste");
        var queueEngine = new QueueEngine();
        queueEngine.AddQueue(newQueue);
        var lenghtQueueList = queueEngine.QueueListLength();
        Assert.Equal(1,lenghtQueueList);
    
    }
    
    [Fact]
    public void Deve_Chamar_Fila_B_Se_Fila_A_Esvaziar()
    {
        var newQueueA = new ClientQueue("A");
        var newQueueB = new ClientQueue("B");
        newQueueB.Enqueue(new Client("Client_B",1));
        var queueEngine = new QueueEngine();
        queueEngine.AddQueue(newQueueA);
        queueEngine.AddQueue(newQueueB);

        var clientCalLed = queueEngine.ProcessQueue();
        
        Assert.Equal("Client_B",clientCalLed.Name);
    
    }
}

