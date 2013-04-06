namespace ExportToPdf
{
    using System;
    using System.Collections.Generic;

    public class Factory
    {
        public static IList<Order> GetDummyOrders()
        {
            var orders = new List<Order>(10);
            var random = new Random();
            for (var i = 0; i < 200; i++)
            {
                var order = Order.Create(random);
                orders.Add(order);
            }
            return orders;
        }
    }
}
