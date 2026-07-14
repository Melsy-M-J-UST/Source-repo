using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Cache;
using System.Reflection.PortableExecutable;
using static DotNetOpenAuth.OpenId.Extensions.AttributeExchange.WellKnownAttributes;
using SqlCommandBuilder = Microsoft.Data.SqlClient.SqlCommandBuilder;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlDataAdapter = Microsoft.Data.SqlClient.SqlDataAdapter;

var config = new ConfigurationBuilder().AddJsonFile("AppSettings. json").Build();
var conString = config.GetConnectionString("DbCon");

SqlConnection con = new SqlConnection(conString);

SqlDataAdapter da = new SqlDataAdapter("Select * from employees", con);
SqlCommandBuilder scb = new SqlCommandBuilder(da);//used to generate the insert, update and delete commands for the data adapter
DataSet ds= new DataSet();
da.Fill(ds);//actual connection to the database and fetching the data into dataset
foreach (DataRow row in ds.Tables[0].Rows)
{
    Console.WriteLine(row["id"]+" " + row[gender]+" " + row[Name]+" " + row[age]);
}


DataRow row1=ds.Tables[0].NewRow();
row1["id"] = 6;
row1["name"] = "Hi";
row1["gender"]= "Female";
row1["age"] = 25;
row1["salary"] = 50000;

ds.Tables[0].Rows.Add(row1);//adding the new row to the dataset
//scb.Update(ds);//updating the database with the new row added to the dataset


//data adapter is used to fetch the data from the database and fill it into the dataset and then we can use the dataset to display the data in the console.

//dataset is an in-memory representation of the data and it can be used to manipulate the data without affecting the database. It is a disconnected architecture because it does not require a continuous connection to the database. We can fetch the data once and then work with it in memory without needing to go back to the database until we want to update or delete the data.

//data adapter and dataset are used together to implement the disconnected architecture in ADO.NET.
//The data adapter is responsible for fetching the data from the database and filling it into the dataset,
//while the dataset is responsible for holding the data in memory and allowing us to manipulate it without affecting the database.
//This allows us to work with the data in a more flexible way and reduces the need for constant database connections, which can improve performance and scalability.


















var config1 = new ConfigurationBuilder().AddJsonFile("AppSettings. json").Build();
var conString1 = config1.GetConnectionString("DbCon");

SqlConnection con1 = new SqlConnection(conString1);

SqlDataAdapter da1 = new SqlDataAdapter("Select * from employees", con1);
da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
SqlCommandBuilder builder1 = new SqlCommandBuilder(da1);

DataSet ds1 = new DataSet();

da1.Fill(ds1);
ds1.Tables[0].PrimaryKey = new DataColumn[]
{
    ds1.Tables[0].Columns["id"]
};
DataRow row2 = ds1.Tables[0].Rows.Find(1);
row1["Salary"] = 65433;
da1.Update(ds1);
Console.WriteLine("Employee updated Successfully");
