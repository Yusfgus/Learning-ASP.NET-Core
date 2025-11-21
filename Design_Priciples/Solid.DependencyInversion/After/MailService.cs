namespace Design_Principles.Solid.DependencyInversion.After;

internal class MailService : INotificationMode
{
    public string Address { get; set; }
    public void Send()
    {
        System.Console.WriteLine($"e-mail is sent to { Address}");
    }
}