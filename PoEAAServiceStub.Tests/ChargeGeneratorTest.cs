using PoEAAServiceStub.Example.Model;
using PoEAAServiceStub.Example.Services;

namespace PoEAAServiceStub.Tests;

public class ChargeGeneratorTest
{
	[Fact]
	public void TestCalculatesChargesForSchedule()
	{
		var schedule = new BillingSchedule(
				productCode: "ABC123",
				address: new Address(street: "1234 Market St", city: "San Francisco", state: "CA"),
				billingAmount: Money.Dollars(100)
		);

		var chargeGenerator = new ChargeGenerator(taxService: new FlatRateTaxService());
		var charges = chargeGenerator.CalculateCharges(schedule);

		Assert.Equal(2, charges.Length);
		Assert.Equal(Money.Dollars(100), charges[0].Amount);
		Assert.Equal(Money.Dollars(5), charges[1].Amount);
	}

	[Fact]
	public void TestConsiderExemptions()
	{
		var exemptProductCode1 = "12300";

		var firstSchedule = new BillingSchedule(
				productCode: exemptProductCode1,
				address: new Address(street: "1234 Market St", city: "San Francisco", state: "CA"),
				billingAmount: Money.Dollars(100)
		);

		var chargeGenerator = new ChargeGenerator(taxService: new ExemptProductTaxService());

		var firstCharges = chargeGenerator.CalculateCharges(firstSchedule);
		Assert.Single(firstCharges);
		Assert.Equal(Money.Dollars(100), firstCharges[0].Amount);
	}
}
