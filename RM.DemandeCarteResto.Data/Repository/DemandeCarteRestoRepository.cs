using Abstraction;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class DemandeCarteRestoRepository : IDemandeCarteRestoRepository
    {
        private readonly DemandeCarteRestoContext _context;
        public DemandeCarteRestoRepository(DemandeCarteRestoContext context)
        {
            _context = context;
        }
  
        public async Task<DemandeCarteRestaurant> AddDemandeCarte(DemandeCarteRestaurant demandeCarteResto)
        {
            if (demandeCarteResto == null)
            {
                throw new ArgumentNullException(nameof(demandeCarteResto));
            }

            var acceptedRequest = await _context.DemandesCarteRestaurant
                .FirstOrDefaultAsync(d => d.UserId == demandeCarteResto.UserId && d.Status == true);

            var pendingRequest = await _context.DemandesCarteRestaurant
                .FirstOrDefaultAsync(d => d.Status == null && d.UserId == demandeCarteResto.UserId);

            if (acceptedRequest == null && pendingRequest == null)
            {
                var result = await _context.DemandesCarteRestaurant.AddAsync(demandeCarteResto);
                await _context.SaveChangesAsync();
                return result.Entity;
            }

            return null;
        }
        public async Task<IEnumerable<DemandeCarteRestaurant>> GetAllDemandes()
        {
            return _context.DemandesCarteRestaurant.AsEnumerable();
        }

        public async Task<IEnumerable<DemandeCarteRestaurant>> GetAllPendigDemandes()
        {
            return await _context.DemandesCarteRestaurant.Where(d => d.Status== null).ToListAsync();
        }
        public async Task<DemandeCarteRestaurant> GetDemandeCardById(string partitionkey)
        {

            return await _context.DemandesCarteRestaurant.WithPartitionKey(partitionkey).FirstAsync();          
        }
        public async  Task<IEnumerable<DemandeCarteRestaurant>> GetDemandeCardByUserId(string UserId)
        {
            return await _context.DemandesCarteRestaurant.Where(d => d.UserId == UserId).ToListAsync();
        }

      

        public async Task RemoveDemandeCard(string partitionkey)
        {
            var card = await _context.DemandesCarteRestaurant.FindAsync(partitionkey);
            if (card == null)
            {
                throw new KeyNotFoundException($"DemandeCard with ID {partitionkey} not found");
            }

            _context.DemandesCarteRestaurant.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDemandeCard(string partitionkey, DemandeCarteRestaurant DemandeCarteRestaurant)
        {
            if (partitionkey != DemandeCarteRestaurant.PartitionKey)
            {
                throw new ArgumentException("DemandeCard ID mismatch");
            }

            _context.Entry(DemandeCarteRestaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandeCardExists(partitionkey))
                {
                    throw new KeyNotFoundException($"Card with ID {partitionkey} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool DemandeCardExists(string partitionkey)
        {
            return _context.DemandesCarteRestaurant.Any(e => e.PartitionKey == partitionkey);
        }
    }
}
