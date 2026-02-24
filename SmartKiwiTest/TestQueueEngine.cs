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
        var lenghtQueueLista = queueEngine.QueueListLength();
        Assert.Equal(1,lenghtQueue);
    
    }
}

