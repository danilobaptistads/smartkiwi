using Moq;
using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;
using SmartKiwiApp.Services;

namespace SmartKiwiTest;

public class ClientQueueServiceTests
{
    private readonly Mock<IClientQueueRepository> _clientQueueRepositoryMock;
    private Guid _wonerId;

    public ClientQueueServiceTests()
    {
        _wonerId = new Guid();
        _clientQueueRepositoryMock = new Mock<IClientQueueRepository>();
    }

    [Fact]
    public async Task Deve_Criar_ClientQueue()
    {
        var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);

        _clientQueueRepositoryMock.Setup(x => x.Add(It.IsAny<ClientQueue>())).Returns(Task.CompletedTask);

        await clientQueueService.CreateNewQueue("FilaTeste", _wonerId, 5);

        _clientQueueRepositoryMock.Verify(x => x.Add(It.IsAny<ClientQueue>()), Times.Once);
    }

}