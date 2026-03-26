namespace SmartKiwiTest;
using SmartKiwiApp.Models;
public class EmptyQueueTests
{
    private readonly int maxWaiteTime;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    private  QueueEngine queueEngine;
    private  List<ClientQueue> queueList;
    public EmptyQueueTests()
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
    public void Deve_Chamar_B_Em_Sequencia_SE_A_Vazia()
    {
        queueA.Enqueue(new Client("A_1", 1));
        queueB.Enqueue(new Client("B_1", 1));
        queueB.Enqueue(new Client("B_2", 1));
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        Client clientCalled;
        queueEngine. InicializeLastcallTime();

        var callsList = new List<string>();
        var expected = new[]{"A_1","B_1","B_2"};
        
         for(int i=0; i<3; i++)
            {
                clientCalled = queueEngine.ProcessClient();
                callsList.Add(clientCalled.Name);
            }

        Assert.Equal(expected, callsList);

    }
    [Fact]
    public void Deve_Retornar_Null_Se_Não_Hover_Clients_Em_Nenhuma_Fila()
    {
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);

        var clientCalled = queueEngine.ProcessClient();
        
        Assert.Null(clientCalled);

    }
    [Theory]
    [InlineData(true,false,false, new[]{"B_1","C_1","B_2"})]
    [InlineData(false,true,false, new[]{"A_1","C_1","A_2","A_3"})]
    [InlineData(false,false,true, new[]{"A_1","B_1","A_2","B_2","A_3"})]
    public void Deve_Chamar_Mesmo_Com_Uma_Fila_Vazia(bool aIsEmpty, bool bIsEmpty,bool cIsEmpty, string[] expected)
    {
    if (!aIsEmpty)
    {
        queueA.Enqueue(new Client("A_1", 1));
        queueA.Enqueue(new Client("A_2", 1));
        queueA.Enqueue(new Client("A_3", 1));
    }

    if (!bIsEmpty)
    {
        queueB.Enqueue(new Client("B_1", 1));
        queueB.Enqueue(new Client("B_2", 1));
    }

    if (!cIsEmpty)
    {
        queueC.Enqueue(new Client("C_1", 1));
    }

    var callsList = new List<string>();

    queueList.Add(queueA);
    queueList.Add(queueB);
    queueList.Add(queueC);
    queueEngine = new QueueEngine(maxWaiteTime,queueList);
    queueEngine.InicializeLastcallTime();

    Client clientCalled;

    while ((clientCalled = queueEngine.ProcessClient()) != null)
    {
        callsList.Add(clientCalled.Name);
    }

    Assert.Equal(expected, callsList);
}

    [Theory]
    [InlineData(true,false,false, new[]{"Null_Aqui","A_1"})]
    [InlineData(false,true,false, new[]{"Null_Aqui","B_1"})]
    [InlineData(false,false,true, new[]{"Null_Aqui","C_1"})]
    public void Deve_Chamar_O_Cliente_Ao_Adicionar_Em_Fila_Vazia(bool addInA, bool addInB,bool AddInC, string[] expected)
    {
        
        Client clientCalled;
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine.InicializeLastcallTime();
        var callsList = new List<string>();
        
        clientCalled = queueEngine.ProcessClient();
        if (clientCalled == null)
        {
            callsList.Add("Null_Aqui");
        }
        else
        {
            callsList.Add(clientCalled.Name);
        }
       

        if (addInA)
        {
            queueA.Enqueue(new Client("A_1", 1));
            
        }

        if (addInB)
        {
            queueB.Enqueue(new Client("B_1", 1));
    
        }

        if (AddInC)
        {
            queueC.Enqueue(new Client("C_1", 1));
        }
        
        clientCalled = queueEngine.ProcessClient();
        callsList.Add(clientCalled.Name);

        Assert.Equal(expected, callsList);
    }

}