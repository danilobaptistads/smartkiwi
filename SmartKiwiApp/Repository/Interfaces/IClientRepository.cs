using SmartKiwiApp.Models;
namespace SmartKiwiApp.Repository;
public interface IClientRepository
{
    public Task Add(Client newClient);
    public Task<IEnumerable<Client>> GetRemainClients(Guid queueId);
    public Task RemoveClient(Client clientToRemove);

}