using PoEAAServiceStub.Example.Model;
namespace PoEAAServiceStub.Example.Services;

public interface ITaxService
{
  public TaxInfo GetSalesTaxInfo(string productCode, Address address, Money saleAmount);
}
