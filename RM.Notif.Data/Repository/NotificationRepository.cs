using Microsoft.EntityFrameworkCore;
using RM.DemandeCarteResto.Data.Data;
using RM.Notif.Abstraction.Repository;
using RM.Notif.Model.Entities;


namespace RM.Notif.Data.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext _context;

        public NotificationRepository(NotificationContext context)
        {
            _context = context;
        }

        public async Task<Notification> addNotification(Notification notification)
        {
            var result = await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Notification>> getAllNotificationByReceiverId(string ReceiverId)
        {
            return await _context.Notifications.Where(n => n.ReceiverId == ReceiverId).ToListAsync();
        }
        public async Task readNotification(string partitionKey)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.PartitionKey == partitionKey);
            Console.WriteLine(notification);
            if (notification != null)
            {
                notification.Read = true;
                await _context.SaveChangesAsync();
            }
          
        }
    }
}
