using System.Collections.Generic;
using System.Data.SqlClient;
using System;

public static class DataReaderExtensions
{
    public static List<Employee> ReadEmployees(this SqlDataReader reader) //fa finta di appartenere a sql datar
    {
        var foundEmployees = new List<Employee>();
        while (reader.Read())
        {
            var nuovoEmp = new Employee();
            var x = reader.GetFieldValue<string>(reader.GetOrdinal("Title"));

            nuovoEmp.Empid = reader.GetFieldValue<Int32>(reader.GetOrdinal("empid"));
            if (reader.IsDBNull(reader.GetOrdinal("mgrid")))
            {
                nuovoEmp.ManagerId = null;
            }
            else
            {
                nuovoEmp.ManagerId = reader.GetFieldValue<int?>(reader.GetOrdinal("mgrid"));
            }
            nuovoEmp.FirstName = reader.GetFieldValue<string>(reader.GetOrdinal("firstname"));
            nuovoEmp.LastName = reader.GetFieldValue<string>(reader.GetOrdinal("lastname"));
            nuovoEmp.Address = reader.GetFieldValue<string>(reader.GetOrdinal("address"));
            nuovoEmp.City = reader.GetFieldValue<string>(reader.GetOrdinal("city"));
            nuovoEmp.Country = reader.GetFieldValue<string>(reader.GetOrdinal("country"));
            nuovoEmp.PostalCode = reader.GetFieldValue<string>(reader.GetOrdinal("postalcode"));
            nuovoEmp.Phone = reader.GetFieldValue<string>(reader.GetOrdinal("phone"));
            nuovoEmp.Title = reader.GetFieldValue<string>(reader.GetOrdinal("Title"));
            nuovoEmp.TitleOfCourtesy = reader.GetFieldValue<string>(reader.GetOrdinal("titleofcourtesy"));
            nuovoEmp.BirthDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("birthdate"));
            nuovoEmp.HireDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("hiredate"));

            foundEmployees.Add(nuovoEmp);

        }
        return foundEmployees;
    }
}