namespace Design_Principles.LooseVsTightCoupling.After;

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

