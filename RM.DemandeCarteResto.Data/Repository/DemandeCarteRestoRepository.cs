using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Data.Data;
using RM.DemandeCarteResto.Model.Entity;


namespace RM.DemandeCarteResto.Data.Repository
{
    public class DemandeCarteRestoRepository : IDemandeCarteRestoRepository
    {
        private readonly DemandeCarteRestoContext _context;
        public DemandeCarteRestoRepository(DemandeCarteRestoContext context)
        {
            _context = context;
        }
        public async Task acceptDemandeCard(string partitionkey)
        {
            var demand = await _context.DemandesCarteRestaurant.FindAsync(partitionkey);
            if (demand == null)
            {
                throw new KeyNotFoundException($"DemandeCard with ID {partitionkey} not found");
            }
            demand.Status = true;
            await _context.SaveChangesAsync();
        }
        public async Task<DemandeCarteRestaurant> addDemandeCarte(DemandeCarteRestaurant demandeCarteResto)
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
        public async Task<IEnumerable<DemandeCarteRestaurant>> getAllDemandes()
        {
            return _context.DemandesCarteRestaurant.AsEnumerable();
        }

        public async Task<IEnumerable<DemandeCarteRestaurant>> getAllPendigDemandes()
        {
            return await _context.DemandesCarteRestaurant.Where(d => d.Status== null).ToListAsync();
        }
        public async Task<DemandeCarteRestaurant> getDemandeCardById(string partitionkey)
        {
            
                return await _context.DemandesCarteRestaurant.FindAsync(partitionkey);
          
        }
        public async  Task<IEnumerable<DemandeCarteRestaurant>> getDemandeCardByUserId(string UserId)
        {
            return await _context.DemandesCarteRestaurant.Where(d => d.UserId == UserId).ToListAsync();
        }

        public async Task rejectDemandeCard(string partitionkey)
        {
            var card = await _context.DemandesCarteRestaurant.FindAsync(partitionkey);
            if (card == null)
            {
                throw new KeyNotFoundException($"DemandeCard with ID {partitionkey} not found");
            }
            card.Status = false;
          
            await _context.SaveChangesAsync();
        }

        public async Task removeDemandeCard(string partitionkey)
        {
            var card = await _context.DemandesCarteRestaurant.FindAsync(partitionkey);
            if (card == null)
            {
                throw new KeyNotFoundException($"DemandeCard with ID {partitionkey} not found");
            }

            _context.DemandesCarteRestaurant.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task updateDemandeCard(string partitionkey, DemandeCarteRestaurant DemandeCarteRestaurant)
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
