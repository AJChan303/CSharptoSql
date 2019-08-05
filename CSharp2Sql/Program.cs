using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharp2Sql {
    class Program {
        static void Main(string[] args) {
            var sql = "Select * from Orders";
            var orders = SelectOrder(sql);
            foreach (var order in orders) {
                Console.WriteLine($"Customer ID is {order.CustomerId} { order.Date}");

            }
            //var customers = SelectCustomer(sql);
            //foreach (var customer in customers) {
            //    Console.WriteLine(customer.Name);
            //}
        }
        static List<Order> SelectOrder(string sql) {
            var connStr = "server=localhost\\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("connection did not open!");
            }
            var orderList = new List<Order>();
            var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note")) ? null : reader["Code"].ToString();
                var customerid = reader.IsDBNull(reader.GetOrdinal("CustomerID"))? 0 :(int)reader["CustomerId"]; // can be null

                var order = new Order(id, date, note, customerid);

                 orderList.Add(order);
            }
            reader.Close();
            connection.Close();
            return orderList;
        }
        static List<Customer> SelectCustomer(string sql) { 
            var connStr = @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
           var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("connection did not open!");
            }
            var customerList = new List<Customer>();
            
            var cmd = new SqlCommand(sql, connection);
            var reader =  cmd.ExecuteReader();
            while (reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))? null:reader["Code"].ToString();

                var customer = new Customer(id, name, city, state, active, code);
                customerList.Add(customer);

               
            }
            
            reader.Close();
            connection.Close();
            return customerList;
        }
    }
}
