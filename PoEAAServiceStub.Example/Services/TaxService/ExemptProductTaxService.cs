using PoEAAServiceStub.Example.Model;
namespace PoEAAServiceStub.Example.Services;

public class ExemptProductTaxService : ITaxService
{
  private static readonly decimal FLAT_RATE = 0.05m;
  private static readonly decimal EXEMPT_RATE = 0.00m;
  private static readonly string[] EXEMPT_STATES = ["CA", "TX"];
  private static readonly string[] EXEMPT_PRODUCTS = ["12300", "12301"];

  public TaxInfo GetSalesTaxInfo(string productCode, Address address, Money saleAmount)
  {
    return EXEMPT_STATES.Contains(address.State) && EXEMPT_PRODUCTS.Contains(productCode)
        ? new TaxInfo(EXEMPT_RATE, Money.Dollars(0))
        : new TaxInfo(FLAT_RATE, saleAmount * FLAT_RATE);
  }
}
