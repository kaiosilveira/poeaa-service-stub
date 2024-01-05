namespace PoEAAServiceStub.Example.Model;

public class Money
{
  public readonly Currency Currency;
  private readonly MidpointRounding RoundingMode;
  private decimal amount;
  public decimal Amount
  {
    get { return Round(amount / GetCentsFactor[Currency]); }
    set { amount = Round(value * GetCentsFactor[Currency]); }
  }

  public Money(decimal amount, Currency currency, MidpointRounding roundingMode = MidpointRounding.ToEven)
  {
    Amount = amount;
    Currency = currency;
    RoundingMode = roundingMode;
  }

  public static readonly Dictionary<Currency, decimal> GetCentsFactor = new()
    {
        { Currency.BRL, 100 },
        { Currency.USD, 100 },
        { Currency.EUR, 100 }
    };

  public static Money Dollars(decimal amount) => new(amount, Currency.USD);

  public static Money BrasilianReal(decimal amount) => new(amount, Currency.BRL);

  public static Money Euros(decimal amount) => new(amount, Currency.EUR);

  public override int GetHashCode() => HashCode.Combine(Amount, Currency);

  public override bool Equals(object? obj)
  {
    if (obj is not Money other) return false;
    return Amount == other.Amount && Currency == other.Currency;
  }

  public static Money operator +(Money a, Money b)
  {
    AssertSameCurrency(a, b);
    return new Money(a.Amount + b.Amount, a.Currency);
  }

  public static Money operator -(Money a, Money b)
  {
    AssertSameCurrency(a, b);
    return new Money(a.Amount - b.Amount, a.Currency);
  }

  public static bool operator >(Money a, Money b)
  {
    AssertSameCurrency(a, b);
    return a.Amount > b.Amount;
  }

  public static bool operator <(Money a, Money b)
  {
    AssertSameCurrency(a, b);
    return a.Amount < b.Amount;
  }

  public static Money operator *(Money a, decimal factor)
  {
    return new Money(a.Amount * factor, a.Currency);
  }

  public Money[] Allocate(params int[] ratios)
  {
    var total = ratios.Sum();
    var remainder = amount;

    var results = new decimal[ratios.Length];
    for (var i = 0; i < ratios.Length; i++)
    {
      var share = amount * ratios[i] / total;
      results[i] = share;
      remainder -= share;
    }

    for (var i = 0; i < remainder; i++)
    {
      results[i] += 1;
    }

    return results.Select(r => new Money(r / GetCentsFactor[Currency], Currency)).ToArray();
  }

  private static void AssertSameCurrency(Money a, Money b)
  {
    if (a.Currency != b.Currency)
    {
      throw new InvalidOperationException("Cannot add Money with different currencies");
    }
  }

  private decimal Round(decimal amount)
  {
    return Math.Round(amount, RoundingMode);
  }
}
