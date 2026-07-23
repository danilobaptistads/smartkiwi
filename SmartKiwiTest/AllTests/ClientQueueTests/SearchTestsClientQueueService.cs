// using Moq;
// using SmartKiwiApp.Models;
// using SmartKiwiApp.Repository;
// using SmartKiwiApp.Services;

// namespace SmartKiwiTest;

// public class SearchTestsClientQueueService
// {
//     private readonly Mock<IClientQueueRepository> _clientQueueRepositoryMock;
//     private Guid _wonerId;

//     public SearchTestsClientQueueService()
//     {
//         _wonerId = new Guid();
//         _clientQueueRepositoryMock = new Mock<IClientQueueRepository>();
//     }

//     [Fact]
//     public async Task Deve_Retornar_Lista_Filas_Do_Usuario()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var expectedQueues = new List<ClientQueue>
//         {
//             new ClientQueue("Fila1", _wonerId, 1),
//             new ClientQueue("Fila2", _wonerId, 2)
//         };
//         _clientQueueRepositoryMock.Setup(x => x.GetQueueByUserId(_wonerId)).ReturnsAsync(expectedQueues);

//         var result = await clientQueueService.GetQueuesOfUser(_wonerId);

//         _clientQueueRepositoryMock.Verify(x => x.GetQueueByUserId(_wonerId), Times.Once);
//     }

//     [Fact]
//     public async Task Deve_Retornar_Lista_Vazia_Quando_Usuario_Sem_Filas()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);

//         _clientQueueRepositoryMock.Setup(x => x.GetQueueByUserId(It.IsAny<Guid>())).ReturnsAsync(new List<ClientQueue>());

//         var result = await clientQueueService.GetQueuesOfUser(Guid.NewGuid());

//         Assert.Empty(result);
//     }

// }