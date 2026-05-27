using Moq;
using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;
using SmartKiwiApp.Services;

namespace SmartKiwiTest;
public class testsCheckinService
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly List<ClientQueue> _clientQueuesList;
    private CheckinService checkinService;
    public testsCheckinService()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _clientQueuesList = new List<ClientQueue>()
        {
            new ClientQueue("Prioridade",Guid.NewGuid(), 2, "PR"),
            new ClientQueue("Normal",Guid.NewGuid(), 2, "No"),

        };
        checkinService =  new CheckinService(_clientQueuesList,_clientRepositoryMock.Object);
    }
    [Fact]
    public void Deve_Criar_Cliente_Com_Nome()
    {
        var selectedQueueId = _clientQueuesList[0].Id; 
        var client = checkinService.Checkin(selectedQueueId, "João");

        Assert.NotNull(client);
        Assert.Equal("João", client.Name);
        Assert.Equal("PR001", client.WaiteTicket);
    }

    [Fact]
    public void Deve_Criar_Cliente_Sem_Nome()
    {

        var selectedQueueId = _clientQueuesList[0].Id; 
        var client = checkinService.Checkin(selectedQueueId);

        Assert.NotNull(client);
        Assert.Null(client.Name);
        Assert.Equal("PR001", client.WaiteTicket);
    }
    
    [Fact]
    public void Deve_Incrementar_Contador_lastTicktNumber()
    {

        var selectedQueueId = _clientQueuesList[0].Id;
        _clientQueuesList[0].LastTicktNumber = 10;
        
        checkinService.Checkin(selectedQueueId);

        Assert.Equal(11, _clientQueuesList[0].LastTicktNumber);
    }

    [Fact]
    public void Deve_Salvar_Cliente_Apos_Criar()
    {
        var selectedQueueId = _clientQueuesList[0].Id;
        _clientRepositoryMock.Setup(x => x.Add(It.IsAny<Client>())).Returns(Task.CompletedTask);
        checkinService.Checkin(selectedQueueId, "Maria");

       _clientRepositoryMock.Verify(x => x.Add(It.IsAny<Client>()), Times.Once);
    }

}
