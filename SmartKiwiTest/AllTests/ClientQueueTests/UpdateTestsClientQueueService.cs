// using Moq;
// using SmartKiwiApp.Models;
// using SmartKiwiApp.Repository;
// using SmartKiwiApp.Services;

// namespace SmartKiwiTest;

// public class UpdateTestsClientQueueService
// {
//     private readonly Mock<IClientQueueRepository> _clientQueueRepositoryMock;
//     private Guid _wonerId;

//     public UpdateTestsClientQueueService()
//     {
//         _wonerId = new Guid();
//         _clientQueueRepositoryMock = new Mock<IClientQueueRepository>();
//     }

//     [Fact]
//     public async Task Deve_Atualizar_Nome_Da_Fila()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var queue = new ClientQueue("NomeAntigo", _wonerId, 1);
//         var queueToUpdate = queue.Id;
//         _clientQueueRepositoryMock.Setup(x => x.GetQueueById(queueToUpdate)).ReturnsAsync(queue);
//         _clientQueueRepositoryMock.Setup(x => x.UpdateQueueName(It.IsAny<ClientQueue>(), It.IsAny<string>())).Returns(Task.CompletedTask);


//         await clientQueueService.UpdateQueueName(queue.Id, "NomeNovo");

//         _clientQueueRepositoryMock.Verify(x => x.UpdateQueueName( queue, "NomeNovo"), Times.Once);
//     }

//     [Fact]
//     public async Task Nao_Deve_Atualizar_Nome_Para_Vazio()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var queue = new ClientQueue("NomeAntigo", _wonerId, 1);
//         var queueToUpdate = queue.Id;
//         _clientQueueRepositoryMock.Setup(x => x.GetQueueById(queueToUpdate)).ReturnsAsync(queue);

//         await Assert.ThrowsAsync<ArgumentException>(() => clientQueueService.UpdateQueueName(queue.Id, ""));

//         _clientQueueRepositoryMock.Verify(x => x.UpdateQueueName(It.IsAny<ClientQueue>(), It.IsAny<string>()), Times.Never);
//     }

//     [Fact]
//     public async Task Deve_Atualizar_Prioridade_Da_Fila()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var queue = new ClientQueue("FilaTeste", _wonerId, 1);
//         _clientQueueRepositoryMock.Setup(x => x.GetQueueById(It.IsAny<Guid>())).ReturnsAsync(queue);
//         _clientQueueRepositoryMock.Setup(x => x.UpdateQueuePriority(It.IsAny<ClientQueue>(), It.IsAny<int>())).Returns(Task.CompletedTask);

//         await clientQueueService.UpdateQueuePriority(queue.Id, 5);

//         _clientQueueRepositoryMock.Verify(x => x.UpdateQueuePriority(queue, 5), Times.Once);
//     }

//     [Fact]
//     public async Task Nao_Deve_Atualizar_Prioridade_Para_Valor_Negativo()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var queue = new ClientQueue("FilaTeste", _wonerId, 1);
//         _clientQueueRepositoryMock.Setup(x => x.GetQueueById(It.IsAny<Guid>())).ReturnsAsync(queue);
//         _clientQueueRepositoryMock.Setup(x => x.UpdateQueuePriority(It.IsAny<ClientQueue>(), It.IsAny<int>())).Returns(Task.CompletedTask);

//         await Assert.ThrowsAsync<ArgumentException>(() => clientQueueService.UpdateQueuePriority(queue.Id, -1));

//         _clientQueueRepositoryMock.Verify(x => x.UpdateQueuePriority(It.IsAny<ClientQueue>(), It.IsAny<int>()), Times.Never);
//     }
    

//     [Fact]
//     public async Task Deve_Deletar_Fila()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var queue = new ClientQueue("FilaTeste", _wonerId, 1);

//         _clientQueueRepositoryMock.Setup(x => x.GetQueueById(It.IsAny<Guid>())).ReturnsAsync(queue);
//         _clientQueueRepositoryMock.Setup(x => x.DeleteQueue(It.IsAny<ClientQueue>())).Returns(Task.CompletedTask);

//         await clientQueueService.DeleteQueue(queue.Id);

//         _clientQueueRepositoryMock.Verify(x => x.DeleteQueue(queue), Times.Once);
//     }

//     [Fact]
//     public async Task Nao_Deve_Deletar_Fila_Inexistente()
//     {
//         var clientQueueService = new ClientQueueService(_clientQueueRepositoryMock.Object);
//         var queue = new ClientQueue("FilaTeste", _wonerId, 1);
//         _clientQueueRepositoryMock.Setup(x => x.GetQueueById(It.IsAny<Guid>())).ReturnsAsync((ClientQueue)null!);

//         await Assert.ThrowsAsync<ArgumentException>(() =>clientQueueService.DeleteQueue(Guid.NewGuid()));

//         _clientQueueRepositoryMock.Verify(x => x.DeleteQueue(It.IsAny<ClientQueue>()), Times.Never);
//     }

//     [Fact]
//     public void Deve_Alterar_Prefixo_Da_Fila()
//     {
//         var queue = new ClientQueue("FilaTeste", _wonerId, 1, "ABC");

//         queue.changePrefix("DEF");

//         Assert.Equal("DEF", queue.Prefix);
//     }

// }
