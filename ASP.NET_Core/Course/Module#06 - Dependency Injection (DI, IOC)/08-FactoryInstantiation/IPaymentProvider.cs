public interface IPaymentProvider
{
    string Pay(decimal amount);
}

public class StripePayment : IPaymentProvider
{
    public string Pay(decimal amount)
    {
        return $"Payment of ${amount} was processed using Strip";
    }
}

public class PaypalPayment : IPaymentProvider
{
    public string Pay(decimal amount)
    {
        return $"Payment of ${amount} was processed using Paypal";
    }
}