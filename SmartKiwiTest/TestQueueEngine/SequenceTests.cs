namespace SmartKiwiTest;
using SmartKiwiApp.Models;
public class SequenceTests
{
    private readonly int maxWaiteTime;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    private  QueueEngine queueEngine;
    private  List<ClientQueue> queueList;
    public SequenceTests()
    {
        queueA = new ClientQueue("A");
        queueB = new ClientQueue("B");
        queueC = new ClientQueue("C");
        queueA.SetPriority(3);
        queueB.SetPriority(2);
        queueC.SetPriority(1);
        queueList = new();
        maxWaiteTime = 10;
    }

    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABAC_Em_Um_Ciclo()
    {
        var callsList = new List<string>();
        queueA.Enqueue(new Client("A_1", 1));
        queueA.Enqueue(new Client("A_2", 1));
        queueA.Enqueue(new Client("A_3", 1));
        queueB.Enqueue(new Client("B_1", 1));
        queueB.Enqueue(new Client("B_2", 1));
        queueC.Enqueue(new Client("C_1", 1));
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine. InicializeLastcallTime();

        for (int i = 0; i < 6; i++)
        {
            var clientCalled = queueEngine.ProcessClient();
            callsList.Add(clientCalled.Name);
        }

        Assert.Collection(
            callsList,
            q => Assert.Same("A_1", q),
            q => Assert.Same("B_1", q),
            q => Assert.Same("A_2", q),
            q => Assert.Same("B_2", q),
            q => Assert.Same("A_3", q),
            q => Assert.Same("C_1", q)

            );
    }
    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABACABABAC_Em_2_Ciclos()
    {
        var callsList = new List<string>();
        queueA.Enqueue(new Client("A_1", 1));
        queueA.Enqueue(new Client("A_2", 1));
        queueA.Enqueue(new Client("A_3", 1));
        queueA.Enqueue(new Client("A_4", 1));
        queueA.Enqueue(new Client("A_5", 1));
        queueA.Enqueue(new Client("A_6", 1));
        queueB.Enqueue(new Client("B_1", 1));
        queueB.Enqueue(new Client("B_2", 1));
        queueB.Enqueue(new Client("B_3", 1));
        queueB.Enqueue(new Client("B_4", 1));
        queueC.Enqueue(new Client("C_1", 1));
        queueC.Enqueue(new Client("C_2", 1));
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine. InicializeLastcallTime();

        for (int i = 0; i < 12; i++)
        {
            var ClientCalled = queueEngine.ProcessClient();
            callsList.Add(ClientCalled.Name);
        }

        Assert.Collection(
            callsList,
            q => Assert.Same("A_1", q),
            q => Assert.Same("B_1", q),
            q => Assert.Same("A_2", q),
            q => Assert.Same("B_2", q),
            q => Assert.Same("A_3", q),
            q => Assert.Same("C_1", q),
            q => Assert.Same("A_4", q),
            q => Assert.Same("B_3", q),
            q => Assert.Same("A_5", q),
            q => Assert.Same("B_4", q),
            q => Assert.Same("A_6", q),
            q => Assert.Same("C_2", q)

            );
   }
    [Fact]
    public void Deve_Chamar_Uma_vez_Cada_Quando_Prioridades_Iguais()
    {
        Client clientCalled;
        var expected = new string[]{"A_1","B_1","C_1"};
        var callsList = new List<string>();
        queueA.SetPriority(1);
        queueB.SetPriority(1);
        queueC.SetPriority(1);
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine. InicializeLastcallTime();
        queueA.Enqueue(new Client("A_1", 1));
        queueB.Enqueue(new Client("B_1", 1));
        queueC.Enqueue(new Client("C_1", 1));
            
        while ((clientCalled = queueEngine.ProcessClient()) != null)
        {
            callsList.Add(clientCalled.Name);
        }
        Assert.Equal(expected, callsList);
    }
    [Fact]
    public void Deve_Chamar_Mesmo_Com_Uma_Fila()
    {
        Client clientCalled;
        var expected = new string[]{"A_1","A_2","A_3","A_4"};
        var callsList = new List<string>();
        queueList.Add(queueA);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);

        queueEngine. InicializeLastcallTime();
        queueA.Enqueue(new Client("A_1", 1));
        queueA.Enqueue(new Client("A_2", 1));
        queueA.Enqueue(new Client("A_3", 1));
        queueA.Enqueue(new Client("A_4", 1));

        while ((clientCalled = queueEngine.ProcessClient()) != null)
        {
            callsList.Add(clientCalled.Name);
        }
        Assert.Equal(expected, callsList);
    }
}