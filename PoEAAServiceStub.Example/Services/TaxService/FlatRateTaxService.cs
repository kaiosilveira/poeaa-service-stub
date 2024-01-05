using PoEAAServiceStub.Example.Model;
namespace PoEAAServiceStub.Example.Services;

public class FlatRateTaxService : ITaxService
{
  private static readonly decimal FLAT_RATE = 0.05m;

  public TaxInfo GetSalesTaxInfo(string productCode, Address address, Money saleAmount)
  {
    return new TaxInfo(FLAT_RATE, saleAmount * FLAT_RATE);
  }
}
