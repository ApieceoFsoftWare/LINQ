using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    internal class Program
    {   
        static bool usingFuncDelegate(Customer c)
        {
            if (c.name[0]== 'A')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool predicateDelegate(Customer customer)
        {
            if(customer.birthDay.Year > 1990)
                return true;
            return false;
        }
        static void Main(string[] args)
        {

            DataSource ds = new DataSource();
            List<Customer> customerList = ds.customerList();

            #region LINQ Sorgularında PredicateDelegate Kullanımı

            // En uzun yoldan yaptık. Burada metodu dışarıda oluşturduk ve nesne örneği aldığımız predicate delegate'e bu metodu parametre olarak verdik. 
            // Bu delegate'in bize sağladıklarını predicate değişkenine atıyoruz ve bunu FindAll metoduna atıyoruz ve istediklerimiz alıyoruz.
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Predicate<Customer> predicate = new Predicate<Customer>(predicateDelegate);
            var usingPredicateDelegate1 = customerList.FindAll(predicate);
            
            // 
            var usingPredicateDelegate2 = customerList.FindAll(new Predicate<Customer>(predicateDelegate)); 
            
            
            #endregion

            #region LINQ Sorgularında FuncDelegate kullanımı

            // => operatörü ile kısa yazımı
            var usingLambda = customerList.Where(i => i.name.StartsWith("A"));

            // => operatörü olmadan yazımı
            Func<Customer, bool> usingFuncDelegate1= new Func<Customer, bool>(usingFuncDelegate);                           
            var usingFuncDelegate2 = customerList.Where(usingFuncDelegate);                                                 

            // En uzun kullanımıdır. Burada Where içine yeni bir func delegate örnekleyip alması gereken parametreleri verip bir de içine fonksiyon veriyoruz.
            var usingFuncDelegate3 = customerList.Where(new Func<Customer, bool>(usingFuncDelegate));                       

            // Üsttekinden daha kısa kullanımıdır. Yukarıda Func Delegate örnekledik. Func Delegate delegate'ti extend ettiği için aldığı parametre ile
            // istediğimiz sorguyu return edersek aslında üstteki ile aynı işi yapmış oluruz.
            var usingFuncDelegate4 = customerList.Where(delegate (Customer c) { return c.name[0] == 'A' ? true : false; }); 

            // Üsttekinden farkı Where içine delegate olduğunu belirmemize gerek yok çünkü program içindekinin delegate olduğunu biliyor.
            var usingFuncDelegate5 = customerList.Where((Customer c) => { return c.name[0] == 'A' ? true : false; });

            // Bir değişken üzerinden lambda operatörü ile istediğimiz bilgiyi sorgu ile return ediyoruz. Lambda burada işlemi kısaltan kodları içeriyor.
            var usingFuncDelegate6 = customerList.Where((m) => { return m.name[0] == 'A' ? true : false; });

            // Yukarıdakinden ve hepsinden daha kısa olan kod tanımı... Burada değişkeni parantez içine almamıza gerek yok ve return dememize de gerek yok.
            // Program bunu kendisi lambda operatörü sayesinde algılıyor. Yukarıdaki kodlar alttaki kodun kısalmasındaki sürecin nasıl olduğunu gösteren
            // süreçtir.
            var usingFuncDelegate7 = customerList.Where(m => m.name[0] == 'A'); // Bu kadar kısaldı 


            #endregion

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
