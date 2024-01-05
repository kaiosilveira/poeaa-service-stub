namespace PoEAAServiceStub.Example.Model;

public class TaxInfo(decimal stateRate, Money stateAmount)
{
  public readonly decimal StateRate = stateRate;
  public readonly Money StateAmount = stateAmount;
}
