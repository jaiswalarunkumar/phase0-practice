using InvoiceOop;

List<Invoice> invoices = new()
{
    new GstInvoice(1000m),
    new ExportInvoice(1000m)
};

foreach (Invoice invoice in invoices)
{
    Console.WriteLine($"{invoice.GetType().Name}: Total = {invoice.GetTotal()}");
}

Console.WriteLine("--- LINQ ---");
Step5Demo.RunLinq();

Console.WriteLine("--- Exception ---");
Step5Demo.RunException();

Console.WriteLine("--- Async ---");
await Step5Demo.RunAsync();