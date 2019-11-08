using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();

            Exercise1();


            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            var NotInStock = DataLoader.LoadProducts().Where(p => p.UnitsInStock == 0);
            PrintProductInformation(NotInStock);

        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            var AvailNThree = DataLoader.LoadProducts().Where(p => p.UnitsInStock != 0 && p.UnitPrice > 3);
            PrintProductInformation(AvailNThree);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            var Wash = DataLoader.LoadCustomers().Where(c => c.Region == "WA");
            PrintCustomerInformation(Wash);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            var nameQuery = from product in DataLoader.LoadProducts()
                            select new
                            {
                                product.ProductName
                            };
            foreach (var p in nameQuery)
            {
                Console.WriteLine("Product Names: {0}", p.ProductName);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            var nameQuery = from prod in DataLoader.LoadProducts()
                            select new
                            {
                                prod.ProductName,
                                prod.Category,
                                prod.ProductID,
                                price = prod.UnitPrice * 1.25m,
                                prod.UnitsInStock
                            };
            foreach (var p in nameQuery)
            {
                Console.WriteLine("Product Names: {0}\n " +
                    "Product Category: {1}\n " +
                    "Product ID: {2}\n " +
                    "Product Price: {3}\n" +
                    " Product Stock: {4}\n  ", p.ProductName, p.Category, p.ProductID, p.price, p.UnitsInStock);

            }
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            var nameQuery = from product in DataLoader.LoadProducts()
                            select new
                            {
                                product.ProductName,
                                product.Category
                            };
            foreach (var p in nameQuery)
            {
                Console.WriteLine("Product Names: {0}\n" +
                    "Product Category: {1}\n", p.ProductName.ToUpper(), p.Category.ToUpper());
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// </summary>
        static void Exercise7()
        {
            var nameQuery = from prod in DataLoader.LoadProducts()
                            select new
                            {
                                prod.ProductName,
                                prod.Category,
                                prod.ProductID,
                                price = prod.UnitPrice,
                                prod.UnitsInStock
                            };
            foreach (var p in nameQuery)
            {
                var ReOrder = p.UnitsInStock > 3 ? "No need to reorder" : "Need to reorder!";
                Console.WriteLine("Product Names: {0}\n " +
                    "Product Category: {1}\n " +
                    "Product ID: {2}\n " +
                    "Product Price: ${3}\n" +
                    " Product Stock: {4}            {5}\n  ", p.ProductName, p.Category, p.ProductID, p.price, p.UnitsInStock, ReOrder);

            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            var nameQuery = from prod in DataLoader.LoadProducts()
                            select new
                            {
                                prod.ProductName,
                                prod.Category,
                                prod.ProductID,
                                price = prod.UnitPrice,
                                prod.UnitsInStock,
                                stockValue = prod.UnitPrice * prod.UnitsInStock
                            };
            foreach (var p in nameQuery)
            {
                Console.WriteLine("Product Names: {0}\n " +
                    "Product Category: {1}\n " +
                    "Product ID: {2}\n " +
                    "Product Price: ${3}\n" +
                    " Product Stock: {4}\n" +
                    "Stock Value: ${5}\n", p.ProductName, p.Category, p.ProductID, p.price, p.UnitsInStock, p.stockValue);

            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            foreach (var i in DataLoader.NumbersA.Where(n => n % 2 == 0))
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            List<Customer> customer = DataLoader.LoadCustomers();

            var result = (from c in customer
                          from o in c.Orders
                          where o.Total < 500
                          select c.CustomerID).Distinct();

            foreach (var c in result)
            {
                Console.WriteLine(c);
            }
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            foreach (var i in DataLoader.NumbersC.Where(n => n % 2 != 0).Take(3))
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            foreach (var i in DataLoader.NumbersB.Skip(3))
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            var Wash = DataLoader.LoadCustomers()
                //.GroupBy(i=>i.CompanyName)
                .Where(c => c.Region == "WA")
                .Select(o => new
                {
                    name = o.CompanyName,
                    order = o.Orders.Max(A => A.OrderDate.Date)
                });
            foreach (var o in Wash)
            {
                Console.WriteLine("Company Name: {0}\n Most Recent Order: {1}", o.name, o.order);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            foreach (var i in DataLoader.NumbersC)
            {
                if (i >= 6)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            foreach (var i in DataLoader.NumbersC.Where(t => t % 3 == 0).Skip(1))
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            var ByName = DataLoader.LoadProducts().OrderBy(x => x.ProductName);
            PrintProductInformation(ByName);
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            var DescendProduct = DataLoader.LoadProducts().OrderByDescending(p => p.UnitsInStock);
            PrintProductInformation(DescendProduct);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            var order = DataLoader.LoadProducts().OrderBy(c => c.Category).ThenByDescending(p => p.UnitPrice);
            PrintProductInformation(order);
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            foreach (var i in DataLoader.NumbersB.Reverse())
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            var catQuery = from cat in DataLoader.LoadProducts()
                           group cat by cat.Category;
            foreach (var custom in catQuery)
            {
                Console.WriteLine(custom.Key);
                foreach (var sub in custom)
                {
                    Console.WriteLine("    {0}", sub.ProductName);
                }
            }

        }



        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {
            var catQuery = from cat in DataLoader.LoadProducts()
                           group cat by cat.Category;
            foreach (var custom in catQuery)
            {
                Console.WriteLine(custom.Key);
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            var isAvail = DataLoader.LoadProducts();
            bool tF = isAvail.Any(x => x.ProductID == 789);

            if (tF == true)
            {
                Console.WriteLine("Product Exists");
            }
            else
            {
                Console.WriteLine("Product Does Not Exist");
            }

        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            var catQuery = from cat in DataLoader.LoadProducts()
                           group cat by cat.Category;
            foreach (var custom in catQuery)
            {
                //Console.WriteLine(custom.Key);
                foreach (var sub in custom)
                {

                    if (sub.UnitsInStock == 0)
                    {
                        Console.WriteLine(custom.Key);
                        break;

                    }

                }
            }

        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            var catQuery = from cat in DataLoader.LoadProducts()
                           group cat by cat.Category;
            foreach (var custom in catQuery)
            {
                //Console.WriteLine(custom.Key);
                foreach (var sub in custom)
                {
                    if (sub.UnitsInStock != 0)
                    {
                        Console.WriteLine(custom.Key);
                        break;
                    }

                }
            }
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            int count = 0;
            foreach (var i in DataLoader.NumbersA.Where(o => o % 2 == 1))
            {
                count++;
            }
            Console.WriteLine(count);
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {
            var result = from c in DataLoader.LoadCustomers()
                         select new
                         {
                             c.CompanyName,
                             a = c.Orders.Count()
                         };
            foreach (var i in result)
            {
                Console.WriteLine("Company Name: {0}\n Amount of Orders: {1}", i.CompanyName, i.a);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            var qry = DataLoader.LoadProducts()
                .GroupBy(x => x.Category)
                .Distinct()
                .Select(g => new
                {
                    name = g.Key,
                    count = g.Count()
                });

            foreach (var result in qry)
            {
                Console.WriteLine("Category: {0}\n     Number of products: {1}", result.name, result.count);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            var qry = DataLoader.LoadProducts()
                .GroupBy(x => x.Category)
                .Select(o => new
                {
                    cat = o.Key,
                    count = o.Sum(s => s.UnitsInStock)
                });


            foreach (var result in qry)
            {
                Console.WriteLine("Category: {0}\n     Number of products: {1}", result.cat, result.count);
            }


        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            var qry = DataLoader.LoadProducts()
            .GroupBy(x => x.Category)
            .Select(o => new
            {
                cat = o.Key,
                price = o.Min(x => x.UnitPrice)
            });
            foreach (var result in qry)
            {
                Console.WriteLine("Category: {0}\n     Lowest Price: {1}", result.cat, result.price);
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            var qry = DataLoader.LoadProducts()
                .GroupBy(x => x.Category)
                .OrderBy(x => x.Average(a => a.UnitPrice)).Take(3);

            foreach (var result in qry)
            {
                PrintProductInformation(result);
                Console.WriteLine(" ");
            }

        }
    }
}
