namespace Encapsulation;

public class EmailNotificationService : INotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine("Email Notification Sent: " + message);
    }
}