// using Moq;
// using SmartKiwiApp.Models;
// using SmartKiwiApp.Repository;
// using SmartKiwiApp.Services;

// namespace SmartKiwiTest;

// public class CreationTestsClientQueueService
// {
//     private readonly Mock<IClientQueueRepository> _clientQueueRepositoryMock;
//     private Guid _wonerId;

//     public CreationTestsClientQueueService()
//     {
//         _wonerId = new Guid();
//         _clientQueueRepositoryMock = new Mock<IClientQueueRepository>();
//     }

//     [Fact]
//     public async Task Deve_Criar_ClientQueue()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);

//         _clientQueueRepositoryMock.Setup(x => x.Add(It.IsAny<ClientQueue>())).Returns(Task.CompletedTask);

//         await clientQueueService.CreateNewQueue("FilaTeste", _wonerId, 5);

//         _clientQueueRepositoryMock.Verify(x => x.Add(It.IsAny<ClientQueue>()), Times.Once);
//     }

//     [Fact]
//     public async Task Nao_Deve_Criar_ClientQueue_Com_Nome_Vazio()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);

//         await Assert.ThrowsAsync<ArgumentException>(() =>clientQueueService.CreateNewQueue("", _wonerId, 5));

//         _clientQueueRepositoryMock.Verify(x => x.Add(It.IsAny<ClientQueue>()), Times.Never);
//     }

//     [Fact]
//     public async Task Nao_Deve_Criar_ClientQueue_Com_Prioridade_Negativa()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);

//         await Assert.ThrowsAsync<ArgumentException>(() => clientQueueService.CreateNewQueue("FilaTeste", _wonerId, -1));

//         _clientQueueRepositoryMock.Verify(x => x.Add(It.IsAny<ClientQueue>()), Times.Never);
//     }
// }