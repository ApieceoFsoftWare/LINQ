using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class DataSource
    {
        List<Customer> list;

        public DataSource()
        {
            list = new List<Customer>();
        }
        public List<Customer> customerList()
        {
            for (int i = 1; i <= 1000; i++)
            {
                Customer customer = new Customer();
                customer.customerNumb = i;
                customer.name = FakeData.NameData.GetFirstName();
                customer.surname = FakeData.NameData.GetSurname();
                customer.birthDay = FakeData.DateTimeData.GetDatetime(new DateTime(1984,1,1), new DateTime(1999,1,1));
                
                customer.Country = FakeData.PlaceData.GetCountry();
                customer.City = FakeData.PlaceData.GetCity();
                customer.Region = FakeData.PlaceData.GetCounty();

                customer.emailAddress = $"{customer.name.ToLower()}.{customer.surname.ToLower()}@{FakeData.NetworkData.GetDomain()}";
                customer.phoneNumber = FakeData.PhoneNumberData.GetPhoneNumber();
                
                
                list.Add(customer);
            }
            return list;
        }
    }
}
