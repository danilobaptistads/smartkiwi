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
        queueA = new ClientQueue("A");
        queueB = new ClientQueue("B");
        queueC = new ClientQueue("C");
        queueA.SetPriority(3);
        queueB.SetPriority(2);
        queueC.SetPriority(1);
    }

    [Fact]
    public void Deve_Chamar_Fila_B_Se_A_For_Vazia()
    {
        queueB.Enqueue(new Client("Client_B", 1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var QueueCalLed = queueEngine.ProcessQueue();

        Assert.Same(queueB, QueueCalLed);

    }

    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABAC_Em_Um_Ciclo()
    {
        var callsList = new List<ClientQueue>();
        queueA.Enqueue(new Client("Client_A_1", 1));
        queueA.Enqueue(new Client("Client_A_2", 1));
        queueA.Enqueue(new Client("Client_A_3", 1));
        queueB.Enqueue(new Client("Client_B_1", 1));
        queueB.Enqueue(new Client("Client_B_2", 1));
        queueC.Enqueue(new Client("Client_C_1", 1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);
        queueEngine.AddQueue(queueC);

        for (int i = 0; i < 6; i++)
        {
            var queuCalled = queueEngine.ProcessQueue();
            callsList.Add(queuCalled);
        }

        Assert.Collection(
            callsList,
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueB, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueB, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueC, q)

            );
    }
    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABACABABAC_Em_2_Ciclos()
    {
        var callsList = new List<ClientQueue>();
        queueA.Enqueue(new Client("Client_A_1", 1));
        queueA.Enqueue(new Client("Client_A_2", 1));
        queueA.Enqueue(new Client("Client_A_3", 1));
        queueA.Enqueue(new Client("Client_A_4", 1));
        queueA.Enqueue(new Client("Client_A_5", 1));
        queueA.Enqueue(new Client("Client_A_6", 1));
        queueB.Enqueue(new Client("Client_B_1", 1));
        queueB.Enqueue(new Client("Client_B_2", 1));
        queueB.Enqueue(new Client("Client_B_3", 1));
        queueB.Enqueue(new Client("Client_B_4", 1));
        queueC.Enqueue(new Client("Client_C_1", 1));
        queueC.Enqueue(new Client("Client_C_2", 1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);
        queueEngine.AddQueue(queueC);

        for (int i = 0; i < 12; i++)
        {
            var QueueCalLed = queueEngine.ProcessQueue();
            callsList.Add(QueueCalLed);
        }

        Assert.Collection(
            callsList,
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueB, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueB, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueC, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueB, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueB, q),
            q => Assert.Same(queueA, q),
            q => Assert.Same(queueC, q)

            );


    }
    [Fact]
    public void Deve_Retornar_Null_Se_Não_Hover_Clients_Em_Nenhuma_Fila()
    {
        var queueCalled = queueEngine.ProcessQueue();
        Assert.Null(queueCalled);

    }

    [Theory]
    [InlineData(true,false,false, new[]{"B","C","B"})]
    [InlineData(false,true,false, new[]{"A","C","A","A"})]
    [InlineData(false,false,true, new[]{"A","B","A","B","A"})]
    public void Deve_Chamar_Mesmo_Com_Uma_Fila_Vazia(bool aIsEmpty, bool bIsEmpty,bool cIsEmpty, string[] expected)
    {
    if (!aIsEmpty)
    {
        queueA.Enqueue(new Client("Client_A_1", 1));
        queueA.Enqueue(new Client("Client_A_2", 1));
        queueA.Enqueue(new Client("Client_A_3", 1));
    }

    if (!bIsEmpty)
    {
        queueB.Enqueue(new Client("Client_B_1", 1));
        queueB.Enqueue(new Client("Client_B_2", 1));
    }

    if (!cIsEmpty)
    {
        queueC.Enqueue(new Client("Client_C_1", 1));
    }

    var callsList = new List<string>();

    queueEngine.AddQueue(queueA);
    queueEngine.AddQueue(queueB);
    queueEngine.AddQueue(queueC);

    ClientQueue queueCalled;

    while ((queueCalled = queueEngine.ProcessQueue()) != null)
    {
        callsList.Add(queueCalled.Name);
    }

    Assert.Equal(expected, callsList);
}
}