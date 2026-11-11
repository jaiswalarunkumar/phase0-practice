namespace InvoiceOop;

public class GstInvoice : Invoice
{
    public GstInvoice(decimal amount) : base(amount)
    {
    }

    public override decimal CalculateTax()
    {
        return Amount * 0.18m;
    }
}