namespace Design_Pattern.LooseVsTightCoupling.Before;

class NotificationService
{
    private EmailService _emailService;
    private SmsService _smsService;

    public NotificationService(EmailService emailService, SmsService smsService)
    {
        _emailService = emailService;
        _smsService = smsService;
    }

    public void Notify()
    {
        _emailService.Send();
        _smsService.Send();
    }
}

