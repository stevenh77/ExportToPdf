using System;

namespace ExportToPdf
{
    public class Order
    {
        public Guid CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public int Units { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public static Order Create(Random random)
        {
            var orderDate = DateTime.Now.AddDays(random.Next(100) * -1);
            var units = random.Next(0, 100);
            var unitPrice = (decimal) GetRandomNumber(random, 0.01, 9999.99);
            var totalPrice = units * unitPrice;

            return new Order()
                {
                    CustomerId = Guid.NewGuid(), OrderDate = orderDate, UnitPrice = unitPrice, TotalPrice = totalPrice
                };
        }

        private static double GetRandomNumber(Random random, double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
