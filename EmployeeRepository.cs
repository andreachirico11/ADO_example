using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class EmployeeRepository
{
    private static readonly string CONN_STRING =
      "Server=127.0.0.1;Database=TSQLFundamentals2008;User Id=sa;Password=SQL_server0%;";
    private static readonly string SELECT_ALL_EMPLOYEES
    = "SELECT * FROM HR.Employees";
    private static readonly string SELECT_ALL_EMPLOYEES_LASTNAME_LIKE
    = "SELECT * FROM HR.Employees where lastname LIKE @chars";
    private static readonly string DELETE_FROM
    = "DELETE FROM HR.Employees where empid =  @idToDelete";
    private static readonly string ADD
    = "INSERT INTO HR.Employees (lastname,firstname,title,titleofcourtesy,birthdate,hiredate,address,city,region,postalcode,country,phone,mgrid) VALUES(@lastname,@firstname,@title,@titleofcourtesy,@birthdate,@hiredate,@address,@city,@region,@postalcode,@country,@phone,@mgrid) ";
    private static readonly string SELECT_BY_TITLE
    = "SELECT * FROM HR.Employees WHERE title = @title";

    private static readonly string ERASEORDERBYEMP
    = "DELETE FROM Sales.Orders where empid =  @idToDelete";


    public List<Employee> All()
    {
       using (SqlCommand cmd = new SqlCommand(SELECT_ALL_EMPLOYEES))
       {
           return Read(cmd);
       }
    }
    private List<Employee> Read(SqlCommand cmd)
    {
        List<Employee> employees = null;
        using (SqlConnection con = new SqlConnection(CONN_STRING))
        {
            Console.WriteLine(CONN_STRING);
            con.Open();
            Console.WriteLine("got connection");
            cmd.Connection = con;
            //SqlCommand cmd = new SqlCommand(SELECT_ALL_EMPLOYEES, con);
            SqlDataReader reader = cmd.ExecuteReader();
            //employees = DataReaderExtensions.ReadEmployees(reader);
            employees = reader.ReadEmployees();
        }
        return employees;
    }

    public List<Employee> AllByLastname(string chars)
    {
        List<Employee> employees = null;
        using (SqlConnection con = new SqlConnection(CONN_STRING))
        {
            Console.WriteLine(CONN_STRING);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(SELECT_ALL_EMPLOYEES_LASTNAME_LIKE, con))
            {
                cmd.Parameters.AddWithValue("@chars", "%" + chars + "%");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //employees = DataReaderExtensions.ReadEmployees(reader);
                    employees = reader.ReadEmployees();                    
                }
            }
        }
        return employees;
    }


    public void Erase(int id)
    {
        SqlTransaction objTrans = null;

        using (SqlConnection cn = new SqlConnection(CONN_STRING))
        {
            try
            {
                cn.Open();
                objTrans = cn.BeginTransaction();

                SqlCommand cm = new SqlCommand(DELETE_FROM, cn, objTrans);
                SqlCommand cm2 = new SqlCommand(ERASEORDERBYEMP, cn, objTrans);

                cm.Parameters.AddWithValue("@idToDelete", id);
                cm2.Parameters.AddWithValue("@idToDelete", id);

                var y = cm2.ExecuteNonQuery();
                var x = cm.ExecuteNonQuery();
                // result(y);
                // result(x);
                objTrans.Commit();
            }
            catch (SqlException e)
            {
                objTrans.Rollback();
                System.Console.WriteLine(e);
            }
        }

    }




    //public void Add(string lastname, string firstname, string title, string titleofcourtesy, string birthdate, string hiredate, string address, string city, string region, string postalcode, string country, string phone, int mgrid)
    public void Add(Employee e)
    {
        using (SqlConnection cn = new SqlConnection(CONN_STRING))
        {
            try
            {
                cn.Open();

                using (SqlCommand cm = new SqlCommand(ADD, cn))
                {
                    //cm.Parameters.AddWithValue("@empid", empid);
                    cm.Parameters.AddWithValue("@lastname", e.LastName);
                    cm.Parameters.AddWithValue("@firstname", e.FirstName);
                    cm.Parameters.AddWithValue("@title", e.Title);
                    cm.Parameters.AddWithValue("@titleofcourtesy", e.TitleOfCourtesy);
                    cm.Parameters.AddWithValue("@birthdate", e.BirthDate);
                    cm.Parameters.AddWithValue("@hiredate", e.HireDate);
                    cm.Parameters.AddWithValue("@address", e.Address);
                    cm.Parameters.AddWithValue("@city", e.City);
                    cm.Parameters.AddWithValue("@region", e.Region);
                    cm.Parameters.AddWithValue("@postalcode", e.PostalCode);
                    cm.Parameters.AddWithValue("@country", e.Country);
                    cm.Parameters.AddWithValue("@phone", e.Phone);
                    cm.Parameters.AddWithValue("@mgrid", e.ManagerId);

                    cm.ExecuteNonQuery();                     
                }
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }


    public List<Employee> SearchByTitle(string title)
    {        
        using (SqlCommand cmd = new SqlCommand(SELECT_BY_TITLE))
        {
             cmd.Parameters.AddWithValue("@title",title);
             return Read(cmd);         
        }        
    }

    public void result(int par)
    {

        if (par != 0)
        {
            System.Console.WriteLine("celofatta");
        }
        else
        {
            System.Console.WriteLine("nocelofatta");
        }

    }








}//class