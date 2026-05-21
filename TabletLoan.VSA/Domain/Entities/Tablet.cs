namespace TabletLoan.VSA.Domain.Entities;

public class Tablet : BaseEntity
{
    public Tablet()
    {
        Loans = [];
    }

    public Tablet(string name, string servoPin, string switchPin,
        string airDroidDeviceId, string airDroidCDeviceId)
    {
        Name = name;
        ServoPin = servoPin;
        SwitchPin = switchPin;
        IsAvailable = true;
        AirDroidDeviceId = airDroidDeviceId;
        AirDroidCDeviceId = airDroidCDeviceId;
    }

    public string Name { get; private set; } = string.Empty;
    public string ServoPin { get; private set; } = string.Empty;
    public string SwitchPin { get; private set;} = string.Empty;
    public bool IsAvailable { get; private set; }
    public string AirDroidDeviceId { get; private set; } = string.Empty;
    public string AirDroidCDeviceId { get; private set; } = string.Empty;
    public ICollection<Loan> Loans { get; set; } = null!;

    public static Tablet Create(string name, string servoPin, string switchPin,
        string airDroidDeviceId, string airDroidCDeviceId)
    {
        return new Tablet(name, servoPin, switchPin, airDroidDeviceId, airDroidCDeviceId);
    }

    public void UpdateAvailability()
    {
        IsAvailable = !IsAvailable;
        UpdateModified();
    }
}