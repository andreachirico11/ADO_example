using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace ADOExamples
{
    class Program
    {
       

        static void Main(string[] args)
        {
            var repo = new EmployeeRepository();
            // var employees = repo.AllByLastname("e");

            var employees = repo.All();

           
           // repo.Erase(1);


            foreach (var item in employees)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }

        //repo.Add("piero","carli","a","b","20081013","20070912","aaaaa","bbbbbb","ccc","asdfai","sfasdf","fff",4);

        //    var x = repo.Search("a");
        //    System.Console.WriteLine(x.LastName);


        }
    }
}
