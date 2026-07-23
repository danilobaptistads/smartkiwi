// using Microsoft.EntityFrameworkCore;
// using SmartKiwiApp.Data;
// using SmartKiwiApp.Models;
// using SmartKiwiApp.Repository;

// namespace SmartKiwiTest;

// public class ClientQueueRepositoryTests
// {
//     private SmartKiwiContext ContextBuilder(string databaseName)
//     {
//         var options = new DbContextOptionsBuilder<SmartKiwiContext>()
//             .UseInMemoryDatabase(databaseName: databaseName)
//             .Options;
//         return new SmartKiwiContext(options);
//     }

//     [Fact]
//     public async Task Deve_Adicionar_Fila_Sem_Prefixo_Ao_Banco()
//     {
//         using var context = ContextBuilder("AddQueueDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var newQueue = new ClientQueue("FilaTeste", ownerId, 5);

//         await repository.Add(newQueue);
//         var queuesInDb = await context.ClientQueues.ToListAsync();

//         Assert.Equivalent(newQueue, queuesInDb[0]);
//     }

//     [Fact]
//     public async Task Deve_Adicionar_Fila_Com_Prefixo()
//     {
//         using var context = ContextBuilder("AddQueueWithPrefixDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var newQueue = new ClientQueue("FilaTeste", ownerId, 5, "A");

//         await repository.Add(newQueue);
//         var queuesInDb = await context.ClientQueues.ToListAsync();

//         Assert.Equal("A", queuesInDb[0].Prefix);
//     }

//     [Fact]
//     public async Task Deve_Retornar_Todas_Filas()
//     {
//         using var context = ContextBuilder("GetAllQueuesDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var queue1 = new ClientQueue("Fila1", ownerId, 3);
//         var queue2 = new ClientQueue("Fila2", ownerId, 5);
//         await repository.Add(queue1);
//         await repository.Add(queue2);

//         var queues = await repository.GetQueues();

//         Assert.Equal(2, queues.Count());
//     }

//     [Fact]
//     public async Task Deve_Retornar_Lista_Vazia_Se_Nao_Houver_Filas()
//     {
//         using var context = ContextBuilder("EmptyQueueListDb");
//         var repository = new ClientQueueRepository(context);

//         var queues = await repository.GetQueues();

//         Assert.Empty(queues);
//     }

//     [Fact]
//     public async Task Deve_Buscar_Fila_Pelo_Id()
//     {
//         using var context = ContextBuilder("GetQueueByIdDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var queue = new ClientQueue("FilaTeste", ownerId, 5);
//         await repository.Add(queue);

//         var retrievedQueue = await repository.GetQueueById(queue.Id);

//         Assert.NotNull(retrievedQueue);
//         Assert.Equal(queue.Id, retrievedQueue.Id);
//     }

//     [Fact]
//     public async Task Deve_Lancar_Excecao_Se_Fila_Nao_Encontrada_Por_Id()
//     {
//         using var context = ContextBuilder("GetQueueByIdNotFoundDb");
//         var repository = new ClientQueueRepository(context);

//         await Assert.ThrowsAsync<InvalidOperationException>(() => repository.GetQueueById(Guid.NewGuid())
//         );
//     }

//     [Fact]
//     public async Task Deve_Buscar_Filas_Pelo_UserId()
//     {
//         using var context = ContextBuilder("GetQueuesByUserDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var otherOwnerId = Guid.NewGuid();
//         var queue1 = new ClientQueue("Fila1", ownerId, 3);
//         var queue2 = new ClientQueue("Fila2", ownerId, 5);
//         var queue3 = new ClientQueue("Fila3", otherOwnerId, 2);
//         await repository.Add(queue1);
//         await repository.Add(queue2);
//         await repository.Add(queue3);

//         var userQueues = await repository.GetQueueByUserId(ownerId);

//         Assert.Equal(2, userQueues.Count());
//         Assert.All(userQueues, q => Assert.Equal(ownerId, q.OwnerId));
//     }

//     [Fact]
//     public async Task Deve_Retornar_Lista_Vazia_Se_UserId_Sem_Filas()
//     {
//         using var context = ContextBuilder("GetQueuesByUserEmptyDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var otherOwnerId = Guid.NewGuid();
//         var queue = new ClientQueue("Fila1", otherOwnerId, 3);
//         await repository.Add(queue);

//         var userQueues = await repository.GetQueueByUserId(ownerId);

//         Assert.Empty(userQueues);
//     }

//     [Fact]
//     public async Task Deve_Alterar_Nome_Da_Fila()
//     {
//         using var context = ContextBuilder("UpdateQueueNameDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var queue = new ClientQueue("FilaTeste", ownerId, 5);
//         await repository.Add(queue);

//         await repository.UpdateQueueName(queue, "FilaRenomeada");

//         var updatedQueue = await repository.GetQueueById(queue.Id);
//         Assert.Equal("FilaRenomeada", updatedQueue.Name);
//     }

//     [Fact]
//     public async Task Deve_Alterar_Prioridade_Da_Fila()
//     {
//         using var context = ContextBuilder("UpdateQueuePriorityDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var queue = new ClientQueue("FilaTeste", ownerId, 5);
//         await repository.Add(queue);

//         await repository.UpdateQueuePriority(queue, 10);

//         var updatedQueue = await repository.GetQueueById(queue.Id);
//         Assert.Equal(10, updatedQueue.Priority);
//     }

//     [Fact]
//     public async Task Deve_Alterar_Prefixo_Da_Fila()
//     {
//         using var context = ContextBuilder("UpdateQueuePrefixDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var queue = new ClientQueue("FilaTeste", ownerId, 5, "A");
//         await repository.Add(queue);

//         await repository.UpdateQueuePrefix(queue, "B");

//         var updatedQueue = await repository.GetQueueById(queue.Id);
//         Assert.Equal("B", updatedQueue.Prefix);
//     }

//     [Fact]
//     public async Task Deve_Deletar_Fila()
//     {
//         using var context = ContextBuilder("DeleteQueueDb");
//         var repository = new ClientQueueRepository(context);
//         var ownerId = Guid.NewGuid();
//         var queue = new ClientQueue("FilaTeste", ownerId, 5);
//         await repository.Add(queue);

//         await repository.DeleteQueue(queue);

//         var queues = await context.ClientQueues.ToListAsync();
//         Assert.Empty(queues);
//     }
// }
