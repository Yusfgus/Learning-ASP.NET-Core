namespace Design_Pattern.LooseVsTightCoupling.After;

class NotificationService
{
    private INotificationMode _notificationMode;

    public NotificationService(INotificationMode notificationMode)
    {
        _notificationMode = notificationMode;
    }

    public void Notify()
    {
        _notificationMode.Send();
    }
}

