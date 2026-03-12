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
        queueB.Enqueue(new Client("Client_B", 1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var clientCalLed = queueEngine.ProcessQueue();

        Assert.Equal("Client_B", clientCalLed.Name);

    }
    [Fact]
    public void Deve_Retornar_Client_A_2()
    {
        queueA.Enqueue(new Client("Client_A_1", 1));
        queueA.Enqueue(new Client("Client_A_2", 1));
        queueB.Enqueue(new Client("Client_B", 1));
        queueEngine.AddQueue(queueA);
        queueEngine.AddQueue(queueB);

        var clientCalLed = queueEngine.ProcessQueue();
        clientCalLed = queueEngine.ProcessQueue();
        clientCalLed = queueEngine.ProcessQueue();

        Assert.Equal("Client_A_2", clientCalLed.Name);

    }
    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABAC_Em_Um_Ciclo()
    {
        var callsList = new List<Client>();
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
            var clientCalLed = queueEngine.ProcessQueue();
            callsList.Add(clientCalLed);
        }

        Assert.Collection(
            callsList,
            c => Assert.Equal("Client_A_1", c.Name),
            c => Assert.Equal("Client_B_1", c.Name),
            c => Assert.Equal("Client_A_2", c.Name),
            c => Assert.Equal("Client_B_2", c.Name),
            c => Assert.Equal("Client_A_3", c.Name),
            c => Assert.Equal("Client_C_1", c.Name)

            );
    }
    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABACABABAC_Em_2_Ciclos()
    {
        var callsList = new List<Client>();
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
            var clientCalLed = queueEngine.ProcessQueue();
            callsList.Add(clientCalLed);
        }

        Assert.Collection(
            callsList,
            c => Assert.Equal("Client_A_1", c.Name),
            c => Assert.Equal("Client_B_1", c.Name),
            c => Assert.Equal("Client_A_2", c.Name),
            c => Assert.Equal("Client_B_2", c.Name),
            c => Assert.Equal("Client_A_3", c.Name),
            c => Assert.Equal("Client_C_1", c.Name),
            c => Assert.Equal("Client_A_4", c.Name),
            c => Assert.Equal("Client_B_3", c.Name),
            c => Assert.Equal("Client_A_5", c.Name),
            c => Assert.Equal("Client_B_4", c.Name),
            c => Assert.Equal("Client_A_6", c.Name),
            c => Assert.Equal("Client_C_2", c.Name)

            );


    }
    [Fact]
    public void Deve_Retornar_Null_Se_Não_Hover_Clients_Em_Nenhuma_Fila()
    {
        var clientCalLed = queueEngine.ProcessQueue();
        Assert.Null(clientCalLed);

    }

    [Theory]
    [InlineData(true,false,false, new[]{"Client_B_1","Client_C_1","Client_B_2"})]
    [InlineData(false,true,false, new[]{"Client_A_1","Client_C_1","Client_A_2","Client_A_3"})]
    [InlineData(false,false,true, new[]{"Client_A_1","Client_B_1","Client_A_2","Client_B_2","Client_A_3"})]
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

    Client clientCalLed;

    while ((clientCalLed = queueEngine.ProcessQueue()) != null)
    {
        callsList.Add(clientCalLed.Name);
    }

    Assert.Equal(expected, callsList);
}
}