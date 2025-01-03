using MassTransit;
using NotificationService.Notifications;
using Shared.Events;

namespace NotificationService.Consumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            // queue-dan mesajimizi alırıq
            var userCreatedEvent = context.Message;

            var notificationMessage = new UserCreatedNotification
            {
                UserId = userCreatedEvent.Id.ToString(),
                Name = userCreatedEvent.Name,
                Surname = userCreatedEvent.Surname,
                Username = userCreatedEvent.Username,
                Message = $"New user {userCreatedEvent.Username} is created"
            };

            await SendNotification(notificationMessage);
        }

        private async Task SendNotification(UserCreatedNotification notification)
        {
            Console.WriteLine(notification.Message);
        }
    }

   
}
