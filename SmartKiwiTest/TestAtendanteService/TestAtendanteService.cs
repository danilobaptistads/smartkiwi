namespace SmartKiwiTest;
using SmartKiwiApp.Models;

public class TesteAtendanteService
{
    private readonly QueueEngine queueEngine;
    private readonly AtendanteService atendanteService;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    private int maxWaiteTimeMinutes;
    private List<ClientQueue> queueList;
    public TesteAtendanteService()
    {
        queueA = new ClientQueue("A");
        queueA.SetPriority(3);
        queueList = new(){queueA};
        maxWaiteTimeMinutes = 10;
        queueEngine = new QueueEngine(maxWaiteTimeMinutes,queueList);
        atendanteService = new AtendanteService(queueEngine);
        queueA.Enqueue(new Client("A_1", 214));
    }

    [Fact]
    public void Deve_Retornar_Chamada_Com_Nome_Ticket_E_Atendente_Corretos()
    {
        var atendante = new Atendante("Atendente","5");
        
        var newCall= atendanteService.ProcessNextCall(atendante);
        
        Assert.Equal("A_1",newCall.Client.Name);
        Assert.Equal(214,newCall.Client.WaiteTicket);
        Assert.Equal(atendante,newCall.Atendante);
        
    }
}