namespace PoEAAServiceStub.Example.Model;

public class BillingSchedule(string productCode, Address address, Money billingAmount)
{
	public readonly string ProductCode = productCode;
	public readonly Address Address = address;

	public readonly Money BillingAmount = billingAmount;
}
