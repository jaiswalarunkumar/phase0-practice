namespace InvoiceOop;

public record InvoiceRow(int Id, string Client, decimal Amount, bool IsPaid);

public class InvoiceNotFoundException : Exception
{
    public InvoiceNotFoundException(int id) : base($"Invoice {id} nahi mila")
    {
    }
}

public static class Step5Demo
{
    public static List<InvoiceRow> GetSampleData()
    {
        return new List<InvoiceRow>
        {
            new(1, "Ravi Traders", 5000m, true),
            new(2, "Sita Stores", 12000m, false),
            new(3, "Ravi Traders", 3000m, false),
            new(4, "Mohan & Co", 8000m, true),
            new(5, "Sita Stores", 2000m, true)
        };
    }

    // ---------- LINQ + Collections ----------
    public static void RunLinq()
    {
        var invoices = GetSampleData();

        var unpaid = invoices.Where(i => !i.IsPaid).OrderByDescending(i => i.Amount);
        Console.WriteLine("Unpaid invoices:");
        foreach (var i in unpaid)
            Console.WriteLine($"  #{i.Id} {i.Client} - {i.Amount}");

        decimal totalUnpaid = invoices.Where(i => !i.IsPaid).Sum(i => i.Amount);
        Console.WriteLine($"Total unpaid: {totalUnpaid}");

        var perClient = invoices
            .GroupBy(i => i.Client)
            .Select(g => new { Client = g.Key, Count = g.Count(), Total = g.Sum(i => i.Amount) });
        Console.WriteLine("Client-wise:");
        foreach (var c in perClient)
            Console.WriteLine($"  {c.Client}: {c.Count} invoices, total {c.Total}");

        Dictionary<int, InvoiceRow> byId = invoices.ToDictionary(i => i.Id);
        Console.WriteLine($"Invoice 4 ka client: {byId[4].Client}");
    }

    // ---------- Exception ----------
    public static InvoiceRow FindInvoice(List<InvoiceRow> list, int id)
    {
        InvoiceRow? found = list.FirstOrDefault(i => i.Id == id);
        if (found is null)
            throw new InvoiceNotFoundException(id);
        return found;
    }

    public static void RunException()
    {
        var invoices = GetSampleData();
        try
        {
            var inv = FindInvoice(invoices, 99);
            Console.WriteLine(inv.Client);
        }
        catch (InvoiceNotFoundException ex)
        {
            Console.WriteLine($"Error pakda: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("finally: ye hamesha chalta hai");
        }
    }

    // ---------- async/await ----------
    public static async Task<List<InvoiceRow>> GetInvoicesFromDbAsync()
    {
        await Task.Delay(1000); // 1 second ka fake DB wait
        return GetSampleData();
    }

    public static async Task RunAsync()
    {
        Console.WriteLine("DB call shuru...");
        var invoices = await GetInvoicesFromDbAsync();
        Console.WriteLine($"DB se {invoices.Count} invoices aaye");
    }
}