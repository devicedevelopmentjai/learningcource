using System;
using System.Collections;
using System.Collections.Generic;

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

    // +++++++++++++++++++++++++++++++++++++++++++++++++++++
    //Excercise 2
    // +++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum Size
    {
        Small, Medium, Large, ExtraLarge
    }
    public enum Color
    {
        Green, Red, Orange, kakhi
    }
    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var item in products)
            {
                if(item.Size == size)
                    yield return item;
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var item in products)
            {
                if(item.Color == color)
                    yield return item;
            }
        }

        /*
            If bose come and ask both size and color, then we have to add combination of size 
            add color.
            But this violate "OCP".
            @@-> we may use Inheritance to get solve this issuse.
            
            ## Good to use Enterprise pattern
            
            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products
            , Color color, Size size)
            {
            foreach (var item in products)
            {
                if(item.Color == color && item.Size == size)
                    yield return item;
            }
        }

        */      

    }

     // Solution
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specific);
    }
    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;
        public ColorSpecification(Color color)
        {
            _color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;
        public SizeSpecification(Size size)
        {
            _size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }
    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> _first, _second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first?? throw new ArgumentNullException(paramName:nameof(first));
            _second = second?? throw new ArgumentNullException(paramName:nameof(second));
        }
        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specific)
        {
            foreach (var item in items)
            {
                if(specific.IsSatisfied(item))
                    yield return item;
            }
        }

        // Do not add any extra, if we wanna new filter then add
    }


    public static class OpenCloseEntry
    {
        public static void Start()
        {
            var apple = new Product("Apple", Color.Red, Size.Medium);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Green, Size.Medium);
            Product[] products = {apple, tree, house};
            var productFilter = new ProductFilter();
            Console.WriteLine("Filter by Size {Medium}");
            foreach (var item in productFilter.FilterBySize(products, Size.Medium))
            {
                Console.WriteLine($"- {item.Name}, {item.Color}, {item.Size}");
            }
            Console.WriteLine("Filter by Color {Green}");
            foreach (var item in productFilter.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($"- {item.Name}, {item.Color}, {item.Size}");
            }

            Console.WriteLine("Open Close Better filter");
            var betterFilter = new BetterFilter();
            foreach (var item in betterFilter.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"- {item.Name}, {item.Color}, {item.Size}");
            }

            Console.WriteLine("Filter by Size {Medium}");
            foreach (var item in betterFilter.Filter(products, new SizeSpecification(Size.Medium)))
            {
                Console.WriteLine($"- {item.Name}, {item.Color}, {item.Size}");
            }

            Console.WriteLine("Combination");
            var andSpecification = new AndSpecification<Product>(
                new ColorSpecification(Color.Green),
                new SizeSpecification(Size.Medium));

            foreach (var item in betterFilter.Filter(products, andSpecification))
            {
                Console.WriteLine($"- {item.Name}, {item.Color}, {item.Size}");
            }
        }
    }

}