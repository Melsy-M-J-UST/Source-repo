using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.PortableExecutable;

var config=new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
var connectionString = config.GetConnectionString("DbCon");
//string connectionString = "Server=.;Database=employeedb;User Id=Melsy;Password=StrongPassword@123;TrustServiceCertificate=True";
//SqlConnection connection = new SqlConnection(connectionString); // established connection to the database


Console.WriteLine("Enter id");
int id=Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter name");
string? name = Console.ReadLine();
Console.WriteLine("Enter gender");
string? gender = Console.ReadLine();
Console.WriteLine("Enter age");
int age = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter salary");
int salary = Convert.ToInt32(Console.ReadLine());




using SqlConnection con = new SqlConnection(connectionString);

SqlCommand cmd = new SqlCommand("Insert into employees values(@id,@name,@gender,@age,@salary)",con);
cmd.Parameters.AddWithValue("@id", id);
cmd.Parameters.AddWithValue("@name", name);
cmd.Parameters.AddWithValue("@gender", gender);
cmd.Parameters.AddWithValue("@age", age);
cmd.Parameters.AddWithValue("@salary", salary);

con.Open();
cmd.ExecuteNonQuery();
Console.WriteLine("Employees Added Successfully");


//update
using SqlConnection con4 = new SqlConnection(connectionString);

SqlCommand cmd4 = new SqlCommand("update employee set salary=@salary where id=@id", con4);
cmd.Parameters.AddWithValue("@id", id);
cmd.Parameters.AddWithValue("@name", name);
cmd.Parameters.AddWithValue("@gender", gender);
cmd.Parameters.AddWithValue("@age", age);       // @objects are removed after the closing braces
cmd.Parameters.AddWithValue("@salary", salary);

con4.Open();
cmd4.ExecuteNonQuery();
Console.WriteLine("Employees Added Successfully");



//delete
using SqlConnection con3 = new SqlConnection(connectionString);

SqlCommand cmd3 = new SqlCommand("Insert into employees values(@id,@name,@gender,@age,@salary)", con3);
cmd.Parameters.AddWithValue("@id", id);
cmd.Parameters.AddWithValue("@name", name);
cmd.Parameters.AddWithValue("@gender", gender);
cmd.Parameters.AddWithValue("@age", age);
cmd.Parameters.AddWithValue("@salary", salary);

con3.Open();
cmd3.ExecuteNonQuery();
Console.WriteLine("Employees Added Successfully");


















//SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", connection); // SQL command to select all records from Employees table
//cmd.Connection = connection;
//cmd.CommandText= "Select * from Employees";    // Alternative way to set the command text

SqlCommand cmd2 = new SqlCommand("select * from Employees where id=@id",con);

cmd.Parameters.AddWithValue("@id", 1);

con.Open();
SqlDataReader reader = cmd2.ExecuteReader();
//while (reader.Read())
//{
//    Console.WriteLine(reader["id"] + " " + reader["name"]+" " + reader["gender"]);
//} // returns boolean
//Console.WriteLine(reader["name"]);
if (reader.Read())
{
    Console.WriteLine(reader["id"] + " " + reader["name"] + " " + reader["gender"]);
}
else
{
    Console.WriteLine("No employees found");
}
con.Close();







using SqlConnection con = new SqlConnection(connectionString);

SqlCommand cmd = new SqlCommand("update employee set salary=@salary where id=@id");





SqlCommand cmd = new SqlCommand("GetSalaryByld", con);
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@id", id);

SqlParameter param = new SqlParameter();
param.ParameterName = "@Salary";
param.Direction = ParameterDirection.Output;
param.SqlDbType = SqlDbType.Decimal;

cmd.Parameters.Add(param);

con.Open();
cmd.ExecuteNonQuery(); [

Console.WriteLine($"Salary of the employee with id {id} is {param.Value}");






    using SqlConnection con = new SqlConnection(conString);
con.Open();
SqlTransaction transaction = con.BeginTransaction();

SqlCommand cmd = new SqlCommand("update employee set salary=@salary where id=@id", con);

SqlCommand cmd1 = new SqlCommand("delete from employee where id=@id", con);
cmd.Transaction = transaction;
cmd1.Transaction = transaction;

cmd.Parameters.AddWithValue("@salary", 57000);
cmd.Parameters.AddWithValue("@id", 1);
cmd1.Parameters.AddWithValue("@id", 5);

cmd.ExecuteNonQuery();
cmd1.ExecuteNonQuery();

transaction.Commit();|