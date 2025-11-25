public class AppSettings
{
    public const string Name = "AppSettings";
    public TimeSpan OpenAt {get; set;}
    public TimeSpan CloseAt {get; set;}
    public TimeSpan DaysOpen {get; set;}
    public bool EnableOnlineBooking {get; set;}
    public int MaxDailyAppointments {get; set;}
}