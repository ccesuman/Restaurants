namespace Encapsulation;

public class Claim
{
    private readonly INotificationService _service;

    public Claim(INotificationService service)
    {
        _service = service;
    }

    public void GetClaimNotification()
    {
        _service.SendNotification("You have a new claim! with this Id: " + Guid.NewGuid());
    }
}