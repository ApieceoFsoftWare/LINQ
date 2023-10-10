using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        static void actionDelegate(Customer customer)
        {
            Console.WriteLine(customer.name + " " + customer.surname );
        }

        static void Main(string[] args)
        {

            // Gerekli iniztialize'lar
            DataSource ds = new DataSource();
            List<Customer> customerList = ds.customerList();

            /*
             * Predicate Delegate her zaman geriye bir bool döner fakat Func Delegate herhangi bir tip dönebilir.
             * Action delegate ise geriye herhangi bir şey dönmez normal şartlarda ...  
             * 3 farklı delegate var : "Func" , "Predicate" , "Action"
             * 
             * Bunun dışında bu yazım stillerinin hepsini bilmekte fayda var. Aslında nereden geldiklerini bilmekte fayda var.
             * Çünkü yeri geldiğinde hepsini kullanmak ya da özelleştirmek gerebilir.
             * 
             * Bu yüzden buraya geri dönüp bakabilir ve tekrar edebilirsin.
             */

            #region Examples

            // Ex1: Müşteri listesi içerisinde bulunan kayıtlardan => 
            //      ismi "a" ile başlayan,                              *    
            //      soyisim değeri içerisinde "e" harfi olan ve         *
            //      doğum yılı 1985'den büyük olan                      *
            //      kayıtları getirin.
            
            // Benim yaptığım...
            var result1_1 = customerList.Where(customer     => customer.name.StartsWith("A") 
                                                            && customer.surname.Contains('e') 
                                                            && customer.birthDay.Year > 1985).Select(customer => customer);

            // Hocanın yaptığı
            var result1_2 = from customer in customerList
                            where   customer.name.StartsWith("A") && 
                                    customer.surname.Contains('e') && 
                                    customer.birthDay.Year > 1985
                            select customer;


            #endregion

            #region LINQ Sorgularında Action Delegate Kullanımı 

            // En uzun hali ile Action Delegate
            Action < Customer > usingActionDelegate1 = new Action<Customer>(actionDelegate);
            customerList.ForEach(usingActionDelegate1);

            // ForEach içinde Action Delegate örneği alarak yapılan yöntem
            customerList.ForEach(new Action<Customer>(actionDelegate));

            // delegate keyword'ü ile kullanılan biçimi
            customerList.ForEach(delegate (Customer customer) { actionDelegate(customer); });

            // delegate keyword'ünü kullanmak istemiyorsak lambda operatörünü kullanmalıyız... 
            customerList.ForEach((Customer customer) => { Console.WriteLine(customer.name + " " + customer.surname); });

            // Customer class'ından olduğunu belirtmeden direkt değişken adı yazarak da işlemlerimizi yapabiliriz. 
            customerList.ForEach((customer) => { Console.WriteLine(customer.name + " " + customer.surname); });

            // Diğer delegate'lerde en son olarak daha kısa bir yöntem görmüştük fakat bu delegate'de en fazla bir üst satırdaki koda kadar
            // kısaltabiliyoruz. Bunun nedeni bir iş yapmamız gerekiyor. Koleksiyon içinde dolaşmak gibi bir durum olsaydı o zaman
            // kısaltabilirdir. :D 


            #endregion

            #region LINQ Sorgularında PredicateDelegate Kullanımı

            // En uzun yoldan yaptık. Burada metodu dışarıda oluşturduk ve nesne örneği aldığımız predicate delegate'e bu metodu parametre olarak verdik. 
            // Bu delegate'in bize sağladıklarını predicate değişkenine atıyoruz ve bunu FindAll metoduna atıyoruz ve istediklerimiz alıyoruz.
            Predicate<Customer> predicate = new Predicate<Customer>(predicateDelegate);
            var usingPredicateDelegate1 = customerList.FindAll(predicate);
            
            // Yukarıdaki kodun bir kısa versiyonu...
            // FindAll metodu içinde predicateDelegate örnekledik ve yine içine metodumuzu yerleştirip usingPredicateDelagate2  değşikenine atadık.
            var usingPredicateDelegate2 = customerList.FindAll(new Predicate<Customer>(predicateDelegate)); 
            
            // FindAll metodu içerisinde bir bool döndürdük. Bunu yaparken herhangi bir new anahtar sözcüğü kullanmadık. Sadece Predicate Delegate yerine delegate kullandık.
            // Hatta metodu da kullanmadık.
            var usingPredicateDelegate3 = customerList.FindAll(delegate (Customer customer) { return customer.birthDay.Year > 1990; });

            // Lambda operatörü ile kullandık. Delegate keyword'ünü belirtmemize de gerek kalmadı.  
            var usingPredicateDelegate4 = customerList.FindAll((Customer customer) => { return customer.birthDay.Year > 1990; });

            // Burada Customer class'ından olduğunu da belirtmemize gerek kalmadı zaten customerList Customer'ları tuttuğundan lambda sayesinde bunu anlamış olduk.
            var usingPredicateDelegate5 = customerList.FindAll((customer) => { return customer.birthDay.Year > 1990; });

            // Artık en yalın hali ile yazmış olduk. => operatörü bunu yapmamıza kolaylık sağlamış oldu.
            var usingPredicateDelegate6 = customerList.FindAll(customer => customer.birthDay.Year > 1990);

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
