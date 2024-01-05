using PoEAAServiceStub.Example.Model;
namespace PoEAAServiceStub.Example.Services;

public class ChargeGenerator(ITaxService taxService)
{
  private readonly ITaxService taxService = taxService;

  public Charge[] CalculateCharges(BillingSchedule schedule)
  {
    var charges = new List<Charge>();
    var baseCharge = new Charge(schedule.BillingAmount);
    charges.Add(baseCharge);

    var info = taxService.GetSalesTaxInfo(
        schedule.ProductCode, schedule.Address, schedule.BillingAmount
    );

    if (info.StateRate > 0)
    {
      var taxCharge = new Charge(info.StateAmount);
      charges.Add(taxCharge);
    }

    return [.. charges];
  }
}
