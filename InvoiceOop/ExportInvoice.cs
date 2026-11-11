namespace InvoiceOop;

public class ExportInvoice : Invoice
{
    public ExportInvoice(decimal amount) : base(amount)
    {
    }

    public override decimal CalculateTax()
    {
        return 0m;
    }
}