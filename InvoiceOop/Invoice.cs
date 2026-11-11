namespace InvoiceOop;

public abstract class Invoice
{
    public decimal Amount { get; }

    protected Invoice(decimal amount)
    {
        Amount = amount;
    }

    public abstract decimal CalculateTax();

    public decimal GetTotal()
    {
        return Amount + CalculateTax();
    }
}