namespace SOLID
{
    public enum InvoiceType
    {
        Final,
        Proposed
    }
    // Before
    public class Invoice
    {
        public double GetDiscound(double amount, InvoiceType invoiceType)
        {
            double _total = 500;
            if(invoiceType == InvoiceType.Final)
                _total = _total - 50;
            else if(InvoiceType.Proposed == invoiceType)
                _total = _total - 250;

            return _total;
        }
    }

    // Ater
    public class InvoiceAfter
    {
        public virtual double GetDiscount(double amount) => 500;
    }

    public class ProposedInvoice : InvoiceAfter
    {
        public ProposedInvoice()
        {
        }
        public override double GetDiscount(double amount) => base.GetDiscount(amount)-50;

    }

    public class FinalInvoice :InvoiceAfter
    {
        public override double GetDiscount(double amount) => base.GetDiscount(amount)-500;
    }

    public class RecurringInvoice : InvoiceAfter
    {
        public override double GetDiscount(double amount) => base.GetDiscount(amount)-100;
    }
}