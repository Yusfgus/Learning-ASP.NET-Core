namespace Design_Principles.Solid.InterfaceSegregation.Before;

interface IEntitlement
{
    decimal CalculatePension();
    decimal CalculateHealthInsurance();
    decimal CalculateRentalSubsidy();
    decimal CalculateBonuses();
    decimal CalculateTransportationReimbursement();

}

