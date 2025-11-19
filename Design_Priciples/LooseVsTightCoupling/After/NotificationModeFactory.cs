namespace Design_Pattern.LooseVsTightCoupling.After;

enum NotificationMode
{
    EMAIL,
    SMS,
}

class NotificationModeFactory
{
    public static INotificationMode Create(NotificationMode notificationMode)
    {
        return notificationMode switch
        {
          NotificationMode.EMAIL => new EmailService(),  
          NotificationMode.SMS => new SmsService(),  
          _ => new EmailService()
        };
    }
}