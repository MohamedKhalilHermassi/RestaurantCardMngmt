using Microsoft.EntityFrameworkCore;
using Abstraction;
using Model;


namespace Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext _context;

        public NotificationRepository(NotificationContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            var result = await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationByReceiverId(string ReceiverId)
        {
            return await _context.Notifications.Where(n => n.ReceiverId == ReceiverId).ToListAsync();
        }
        public async Task ReadNotification(string partitionKey)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.PartitionKey == partitionKey);
            if (notification != null)
            {
                notification.Read = true;
                await _context.SaveChangesAsync();
            }
          
        }
    }
}
