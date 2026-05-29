using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;

namespace SmartKiwiApp.Repository;
public class ClientRepository : IClientRepository
{
    protected readonly SmartKiwiContext _context;

    public ClientRepository(SmartKiwiContext context)
    {
        _context = context;
    }

    public async Task Add(Client newClient)
    {
        await _context.Clients.AddAsync(newClient);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Client>> GetRemainClients(Guid queueId)
    {
        return await _context.Clients.Where(c => c.QueueId == queueId).ToListAsync();
    }

    public async Task RemoveClient(Client clientToRemove)
    {
        _context.Clients.Remove(clientToRemove);
        await _context.SaveChangesAsync();
    }
}
