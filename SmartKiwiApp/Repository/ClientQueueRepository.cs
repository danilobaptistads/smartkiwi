using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;

namespace SmartKiwiApp.Repository;

public class ClientQueueRepository : IClientQueueRepository
{
    protected readonly SmartKiwiContext _context;

    public ClientQueueRepository(SmartKiwiContext context)
    {
        _context = context;
    }

    public async Task Add(ClientQueue newClientQueue)
    {
        await _context.ClientQueues.AddAsync(newClientQueue);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ClientQueue>> GetQueues()
    {
        return await _context.ClientQueues.ToListAsync();
    }

    public async Task<ClientQueue> GetQueueById(Guid queueId)
    {
        var queue = await _context.ClientQueues.FirstOrDefaultAsync(q => q.Id == queueId);
        if (queue == null)
        {
            throw new InvalidOperationException("Fila não encontrada");
        }
        return queue;
    }

    public async Task<IEnumerable<ClientQueue>> GetQueueByUserId(Guid ownerId)
    {
        return await _context.ClientQueues.Where(q => q.OwnerId == ownerId).ToListAsync();
    }

    public async Task UpdateQueueName(ClientQueue queueToUpdate, string newName)
    {
        _context.Entry(queueToUpdate).Property(q => q.Name).CurrentValue = newName;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQueuePriority(ClientQueue queueToUpdate, int priority)
    {
        queueToUpdate.SetPriority(priority);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQueuePrefix(ClientQueue queueToUpdate, string prefix)
    {
        queueToUpdate.changePrefix(prefix);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteQueue(ClientQueue queueToDelete)
    {
        _context.ClientQueues.Remove(queueToDelete);
        await _context.SaveChangesAsync();
    }
}
