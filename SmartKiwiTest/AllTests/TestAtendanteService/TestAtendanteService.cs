using SmartKiwiApp.Repository;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using Moq;

namespace SmartKiwiTest;
public class TesteAtendanteService
{
    private readonly QueueEngine queueEngine;
    private readonly AtendanteService atendanteService;
    private readonly ClientQueue queueA;
    private int maxWaiteTimeMinutes;
    private List<ClientQueue> queueList;
    private Mock<IClientRepository> _clientRepositoryMock;
    public TesteAtendanteService()
    {
        queueA = new ClientQueue("A", new Guid(),3);
        queueList = new(){queueA};
        maxWaiteTimeMinutes = 10;
        queueEngine = new QueueEngine(maxWaiteTimeMinutes,queueList);
        _clientRepositoryMock = new Mock<IClientRepository>();
        atendanteService = new AtendanteService(queueEngine, _clientRepositoryMock.Object);
        queueA.Enqueue(new Client("TICKT", "A_1"));
    }

    [Fact]
    public void Deve_Retornar_Chamada_Com_Nome_Ticket_E_Atendente_Corretos()
    {
        var atendante = new Atendante("Atendente",5);
        
        var newCall= atendanteService.ProcessNextCall(atendante);
        
        Assert.Equal("A_1",newCall!.ClientName);
        Assert.Equal(atendante.Name, newCall.AtendanteName);
        Assert.Equal(atendante.TicketWindow, newCall.TicketWindowNumber);
        
    }

    [Fact]
    public void Deve_Retornar_Null_Na_Segunda_Chamada()
    {
        var atendante = new Atendante("Atendente",5);
        
        atendanteService.ProcessNextCall(atendante);
        var secondCall= atendanteService.ProcessNextCall(atendante);
        Assert.Null(secondCall);
      
    }

    [Fact]
    public void Deve_Remover_Client_Do_Banco_Apos_Chamada()
    {
        var atendante = new Atendante("Atendente",5);
        
        atendanteService.ProcessNextCall(atendante);
        _clientRepositoryMock.Setup(x => x.RemoveClient(It.IsAny<Client>())).Returns(Task.CompletedTask);
        
        _clientRepositoryMock.Verify(x => x.RemoveClient(It.IsAny<Client>()), Times.Once);
      
    }

}