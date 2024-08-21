using RM.CarteResto.Abstraction;
using Microsoft.EntityFrameworkCore;
using RM.CarteResto.Model;


namespace RM.CarteResto.Data
{
    public class CarteRestoRepository : ICarteRestoRepository
    {
        private readonly CarteRestoContext _context;
        public CarteRestoRepository(CarteRestoContext context)
        {
            _context = context;
        }
        public async Task<CarteRestaurant> AddCard(CarteRestaurant carteResto)
        { 
            var result = await _context.CartesRestaurant.AddAsync(carteResto);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<CarteRestaurant>> GetAllCards()
        {
            return  _context.CartesRestaurant.AsEnumerable();

        }
        public async Task<CarteRestaurant> GetCard(string partitionKey)
        {
            return await _context.CartesRestaurant.WithPartitionKey(partitionKey).FirstAsync();
        }
        public async Task<CarteRestaurant> GetCardByUserId(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new ArgumentException("UserId cannot be null or empty", nameof(UserId));
            }

            var card = await _context.CartesRestaurant
                                     .FirstOrDefaultAsync(c => c.UserId == UserId);

            if (card == null)
            {
                throw new InvalidOperationException($"No card found for user ID {UserId}");
            }

            return card;
        }

        public async Task RemoveCard(string partitionkey)
        {
            var card = await _context.CartesRestaurant.FindAsync(partitionkey);
            if (card == null)
            {
                throw new KeyNotFoundException($"Card with ID {partitionkey} not found");
            }
            _context.CartesRestaurant.Remove(card);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateCard(string partitionkey, CarteRestaurant card)
        {
            if (partitionkey != card.PartitionKey)
            {
                throw new ArgumentException("Card ID mismatch");
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(partitionkey))
                {
                    throw new KeyNotFoundException($"Card with ID {partitionkey} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CardExists(string partitionkey)
        {
            return _context.CartesRestaurant.Any(e => e.PartitionKey == partitionkey);
        }
    }
}
