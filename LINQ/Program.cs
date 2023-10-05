using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DataSource ds = new DataSource();
            List<Customer> customerList = ds.customerList();

            #region Examples
            // 1: Müşteriler içerisinde ülke değeri A ile başlayan müşterileri LINQ to Metode kullanarak yapalım...
            IEnumerable<Customer> customerListExample1= customerList.Where(i => i.Country.StartsWith("A"));
            Console.WriteLine(customerListExample1.Count());

            // 2: customerList içerisindeki
            // kayıtlardan isminin içinde B harfi geçen ve
            // ülke değeri içinde A harfi bulunan müşterilerin listesini getirin...

            List<Customer> customerListExample2_1 = customerList.Where(I => I.name.Contains("b") && I.Country.Contains("a")).ToList();
            
            IEnumerable<Customer> customerListExample2_2 =  from    customer in customerList
                                                            where   customer.name.Contains("b") && customer.Country.Contains("a")
                                                            select  customer;

            // 3: customerList içerisindeki kayıtlardan doğum yılı 1990 büyük olan ve isminin içerisinde a harfi geçen müşterileri LINQ to Query ile bulalım.
            
            var customerListExample3 = from customer in customerList
                                       where customer.birthDay.Year > 1990 && customer.name.Contains("a") 
                                       select customer;

            // 4: Yukarıdaki örneği veya ile yapalım.

            var customerListExample4 = from customer in customerList
                                       where customer.birthDay.Year > 1990 || customer.name.Contains("a")
                                       select customer;
                                       
            #endregion

                                       #region LINQ Query Types
            // 1.Yol Genelde bu yol tercih edilir!
            int result1 = customerList.Where(I => I.name.StartsWith("A")).Count();

            // 2.Yol
            var result2 = (from I in customerList
                           where I.name.StartsWith("A")
                           select I);
            int result3 = result2.Count();

            #endregion
            
            #region At the Beginning
            
            Console.WriteLine(customerList.Count);
            // LINQ sorgusuna geçmeden önce bir alıştırma yapalım

            //int count=0;
            //foreach (var item in customerList)
            //{
            //    if (item.name[0] == 'A')
            //    {
            //        count++;
            //    }
            //}
            //Console.WriteLine(count);

            int count = 0;
            count = customerList.Where(i=>i.name.StartsWith("A")).Count(); // Bu şekilde bir yazım sitili var!
            Console.WriteLine(count);
            #endregion
        }
    }
}
