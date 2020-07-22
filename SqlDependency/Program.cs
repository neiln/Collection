using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;

namespace SqlDependency
{
    class Program
    {
        private static string _con = "data source=.; initial catalog=test_temp; integrated security=True;";
        private static Queue<Customer> customerQue = new Queue<Customer>();
        static void Main(string[] args)
        {
            Setup(); 
        }

        private static void Setup()
        {
            var mapper = new ModelToTableMapper<Customer>();
            mapper.AddMapping(c => c.Surname, "LastName");
            mapper.AddMapping(c => c.Name, "FirstName");

            using (var dep = new SqlTableDependency<Customer>(_con, "Customer", mapper: mapper))
            {
                dep.OnChanged += (s, e) =>
                {

                    Customer customer = e.Entity;
                    Console.WriteLine("DML Op: {0}", e.ChangeType);
                    Console.WriteLine("ID: {0}", customer.Id);
                    Console.WriteLine("First Name: {0}", customer.Name);
                    Console.WriteLine("Last Name: {0}", customer.Surname);
                    customerQue.Enqueue(customer);
                };
                dep.Start();


                Timer timer = new Timer((arg) => {

                    ProcessData();

                }, null, 0, 5000);


                Console.WriteLine("Press a key to exit");
                Console.ReadKey();

            

                dep.Stop();
            }

           
        }
        private static void ProcessData()
        {
            if (customerQue.Count() == 0) return;
            Customer cust = customerQue.Peek();
            Console.WriteLine($"Que Size:{ customerQue.Count() } Next: {cust.Name} ({cust.Id})");
            var timer = new System.Threading.Timer(async (e) =>
            {
                cust = customerQue.Dequeue();
                await Task.Delay(4000);
                
            }, null, 0, 5000);

        
        }


    }
}