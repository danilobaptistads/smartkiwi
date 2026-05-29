using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

namespace SmartKiwiTest;

public class ClientRepositoryTests
{
    private SmartKiwiContext ContextBuilder(string databaseName)
    {
        var options = new DbContextOptionsBuilder<SmartKiwiContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;
        return new SmartKiwiContext(options);
    }

    [Fact]
    public async Task Deve_Adicionar_Cliente_Ao_Banco()
    {
        using var context = ContextBuilder("AddClientDb");
        var repository = new ClientRepository(context);
        var client = new Client(Guid.NewGuid(), "A001");

        await repository.Add(client);
        var clientsInDb = await context.Clients.ToListAsync();

        Assert.Equivalent(client, clientsInDb[0]);
    }

    [Fact]
    public async Task Deve_Adicionar_Cliente_Com_Nome_Ao_Banco()
    {
        using var context = ContextBuilder("AddClientWithNameDb");
        var repository = new ClientRepository(context);
        var client = new Client(Guid.NewGuid(), "A002", "João");

        await repository.Add(client);
        var clientsInDb = await context.Clients.ToListAsync();

        Assert.Equal("João", clientsInDb[0].Name);
    }

    [Fact]
    public async Task Deve_Retornar_Clientes_Remanescentes()
    {
        using var context = ContextBuilder("GetRemainClientsDb");
        var repository = new ClientRepository(context);
        var queueId = Guid.NewGuid();
        var client1 = new Client(queueId, "A001");
        var client2 = new Client(queueId, "A002");
        var client3 = new Client(queueId, "A003");
        await repository.Add(client1);
        await repository.Add(client2);
        await repository.Add(client3);

        var clients = await repository.GetRemainClients(queueId);

        Assert.Equal(3, clients.Count());
    }

    [Fact]
    public async Task Deve_Retornar_Lista_Vazia_Se_Nao_Houver_Clientes()
    {
        using var context = ContextBuilder("EmptyClientListDb");
        var repository = new ClientRepository(context);
        var queueId = Guid.NewGuid();

        var clients = await repository.GetRemainClients(queueId);

        Assert.Empty(clients);
    }

    [Fact]
    public async Task Deve_Remover_Cliente_Do_Banco()
    {
        using var context = ContextBuilder("RemoveClientDb");
        var repository = new ClientRepository(context);
        var client = new Client(Guid.NewGuid(), "A001");
        await repository.Add(client);

        await repository.RemoveClient(client);

        var clientsInDb = await context.Clients.ToListAsync();
        Assert.Empty(clientsInDb);
    }

    [Fact]
    public async Task Deve_Remover_Apenas_Cliente_Especifico()
    {
        using var context = ContextBuilder("RemoveSpecificClientDb");
        var repository = new ClientRepository(context);
        var client1 = new Client(Guid.NewGuid(), "A001");
        var client2 = new Client(Guid.NewGuid(), "A002");
        await repository.Add(client1);
        await repository.Add(client2);

        await repository.RemoveClient(client1);

        var clientsInDb = await context.Clients.ToListAsync();
        Assert.Single(clientsInDb);
        Assert.Equal("A002", clientsInDb[0].WaiteTicket);
    }
}
