using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.PortableExecutable;

string connectionString = "Server=.;Database=Products;User Id=Melsy;Password=StrongPassword@123;TrustServerCertificate=True";

//1.1
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection Successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Connection Failed!");
        Console.WriteLine("Error: " + ex.Message);
    }
}

//1.2
SqlConnection connection1 = new SqlConnection(connectionString);

try
{
    connection1.Open();
    Console.WriteLine("Connection Successful!");
}
catch (Exception ex)
{
    Console.WriteLine("Connection Failed!");
    Console.WriteLine("Error: " + ex.Message);
}
finally
{
    if (connection1.State == System.Data.ConnectionState.Open)
    {
        connection1.Close();
    }
}
//using block automatically handles cleanup. It guarantees that resources are released even if an exception occurs, minimizes the chance of forgetting to close the connection, and results in cleaner, more maintainable code compared to manual management.

//1.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection Successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Connection Failed!");
        Console.WriteLine("Error: " + ex.Message);
    }
}
//changed server=hi in connectionstring
//Error: A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server)

//1.4
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connected");

        // Simulate an error
        throw new Exception("Simulated error");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception caught: " + ex.Message);
    }
}

// Outside using block
SqlConnection testConnection = new SqlConnection(connectionString);

// Print connection state after using block
Console.WriteLine("Connection State after using block: " + testConnection.State);

//1.5
for (int i = 1; i <= 3; i++)
{
    SqlConnection connection = new SqlConnection(connectionString);

    try
    {
        connection.Open();
        Console.WriteLine($"Iteration {i}: Opened");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
    finally
    {
        connection.Close();
        Console.WriteLine($"Iteration {i}: Closed");
    }
}

//2.1

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT ProductID, ProductName, Category, Price, Stock FROM Products";

        SqlCommand command = new SqlCommand(query, connection);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//2.2

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT ProductID, ProductName, Category, Price, Stock FROM Products WHERE Category = @cat";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@cat", "Electronics");

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//2.3

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"SELECT ProductID, ProductName, Category, Price, Stock 
                         FROM Products 
                         WHERE Price BETWEEN @min AND @max";

        SqlCommand command = new SqlCommand(query, connection);

        // Add parameter values
        command.Parameters.AddWithValue("@min", 500);
        command.Parameters.AddWithValue("@max", 3000);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//2.4

using (SqlConnection connection = new SqlConnection(connectionString))

    try
    {
        connection.Open();

        string query = @"SELECT ProductID, ProductName, Category, Price, Stock 
                        FROM Products 
                        WHERE Stock < @stockLimit";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@stockLimit", 50);

        SqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("Low Stock Products:");

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }

//2.5

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"SELECT ProductID, ProductName, Category, Price, Stock 
                         FROM Products 
                         ORDER BY Price ASC";

        SqlCommand command = new SqlCommand(query, connection);

        SqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("Products Sorted by Price (Ascending):");

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//2.6
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT ProductID, ProductName, Category, Price, Stock FROM Products";

        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        // Header
        Console.WriteLine(
            "{0,-10} {1,-18} {2,-15} {3,10} {4,8}",
            "ProductID", "ProductName", "Category", "Price", "Stock");

        Console.WriteLine(new string('-', 65));

        // Data rows
        while (reader.Read())
        {
            Console.WriteLine(
                "{0,-10} {1,-18} {2,-15} {3,10:F2} {4,8}",
                reader["ProductID"],
                reader["ProductName"],
                reader["Category"],
                reader["Price"],
                reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//3.1

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                         VALUES (@name, @category, @price, @stock)";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@name", "Smart Watch");
        command.Parameters.AddWithValue("@category", "Electronics");
        command.Parameters.AddWithValue("@price", 8999);
        command.Parameters.AddWithValue("@stock", 30);

        int rowsInserted = command.ExecuteNonQuery();

        Console.WriteLine(rowsInserted + " row(s) inserted successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//3.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string insertQuery = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                               VALUES (@name, @category, @price, @stock)";

        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

        insertCommand.Parameters.AddWithValue("@name", "Backpack");
        insertCommand.Parameters.AddWithValue("@category", "Accessories");
        insertCommand.Parameters.AddWithValue("@price", 1499);
        insertCommand.Parameters.AddWithValue("@stock", 40);

        insertCommand.ExecuteNonQuery();

        string selectQuery = "SELECT * FROM Products WHERE ProductName = @name";

        SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
        selectCommand.Parameters.AddWithValue("@name", "Backpack");

        SqlDataReader reader = selectCommand.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//3.3
static void InsertProduct(SqlConnection conn, string name, string category, decimal price, int stock)
{
    string query = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                     VALUES (@name, @category, @price, @stock)";

    SqlCommand command = new SqlCommand(query, conn);

    command.Parameters.AddWithValue("@name", name);
    command.Parameters.AddWithValue("@category", category);
    command.Parameters.AddWithValue("@price", price);
    command.Parameters.AddWithValue("@stock", stock);

    command.ExecuteNonQuery();
}

// Call from Main()
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        InsertProduct(connection, "Tablet", "Electronics", 25000, 15);

        Console.WriteLine("Product inserted using method.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//3.4
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                         VALUES (@name, @category, @price, @stock)";

        int totalRows = 0;

        string[] names = { "Ruler", "Eraser", "Sharpener" };
        decimal[] prices = { 20, 15, 25 };
        int[] stocks = { 300, 400, 350 };

        for (int i = 0; i < names.Length; i++)
        {
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", names[i]);
            command.Parameters.AddWithValue("@category", "Stationery");
            command.Parameters.AddWithValue("@price", prices[i]);
            command.Parameters.AddWithValue("@stock", stocks[i]);

            totalRows += command.ExecuteNonQuery();
        }

        Console.WriteLine("Total rows inserted: " + totalRows);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//3.5
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                         VALUES (@name, @category, @price, @stock);
                         SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@name", "Power Bank");
        command.Parameters.AddWithValue("@category", "Electronics");
        command.Parameters.AddWithValue("@price", 1999);
        command.Parameters.AddWithValue("@stock", 20);

        object result = command.ExecuteScalar();

        int newId = Convert.ToInt32(result);

        Console.WriteLine("Inserted product ID = " + newId);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//4.1

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "UPDATE Products SET Price = @price WHERE ProductName = @name";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@price", 999);
        command.Parameters.AddWithValue("@name", "Mouse");

        int rowsUpdated = command.ExecuteNonQuery();

        Console.WriteLine(rowsUpdated + " row(s) updated.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//4.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string updateQuery = "UPDATE Products SET Stock = @stock WHERE ProductName = @name";

        SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
        updateCmd.Parameters.AddWithValue("@stock", 25);
        updateCmd.Parameters.AddWithValue("@name", "Laptop");

        updateCmd.ExecuteNonQuery();

        string selectQuery = "SELECT * FROM Products WHERE ProductName = @name";

        SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
        selectCmd.Parameters.AddWithValue("@name", "Laptop");

        SqlDataReader reader = selectCmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
//4.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"UPDATE Products 
                         SET Price = Price * 1.10 
                         WHERE Category = @cat";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@cat", "Clothing");

        int rowsUpdated = command.ExecuteNonQuery();

        Console.WriteLine(rowsUpdated + " row(s) updated.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//4.4

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"UPDATE Products 
                         SET Stock = Stock + 50 
                         WHERE Category = @cat";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@cat", "Stationery");

        int rowsUpdated = command.ExecuteNonQuery();

        Console.WriteLine(rowsUpdated + " row(s) updated.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//4.5
static int UpdatePrice(SqlConnection conn, int productId, decimal newPrice)
{
    string query = "UPDATE Products SET Price = @price WHERE ProductID = @id";

    SqlCommand command = new SqlCommand(query, conn);

    command.Parameters.AddWithValue("@price", newPrice);
    command.Parameters.AddWithValue("@id", productId);

    int rows = command.ExecuteNonQuery();

    if (rows == 0)
    {
        Console.WriteLine("Product not found");
    }

    return rows;
}

// Call from Main()
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        int result = UpdatePrice(connection, 1, 60000);

        Console.WriteLine("Rows affected: " + result);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//4.6
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string updateQuery = @"UPDATE Products 
                               SET Category = @newCat 
                               WHERE ProductName = @name AND Category = @oldCat";

        SqlCommand updateCmd = new SqlCommand(updateQuery, connection);

        updateCmd.Parameters.AddWithValue("@newCat", "Literature");
        updateCmd.Parameters.AddWithValue("@name", "Novel");
        updateCmd.Parameters.AddWithValue("@oldCat", "Books");

        int rowsUpdated = updateCmd.ExecuteNonQuery();

        Console.WriteLine(rowsUpdated + " row(s) updated.");

        string selectQuery = "SELECT * FROM Products WHERE ProductName = @name";

        SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
        selectCmd.Parameters.AddWithValue("@name", "Novel");

        SqlDataReader reader = selectCmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                "ID: " + reader["ProductID"] +
                ", Name: " + reader["ProductName"] +
                ", Category: " + reader["Category"] +
                ", Price: " + reader["Price"] +
                ", Stock: " + reader["Stock"]);
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//5.1

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "DELETE FROM Products WHERE ProductName = @name";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@name", "Smart Watch");

        int rowsDeleted = command.ExecuteNonQuery();

        Console.WriteLine(rowsDeleted + " row(s) deleted.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//5.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"DELETE FROM Products 
                         WHERE Category = @cat AND Stock > @stock";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@cat", "Stationery");
        command.Parameters.AddWithValue("@stock", 300);

        int rowsDeleted = command.ExecuteNonQuery();

        Console.WriteLine(rowsDeleted + " row(s) deleted.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//5.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "DELETE FROM Products WHERE ProductID = @id";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@id", 10);

        int rowsDeleted = command.ExecuteNonQuery();

        if (rowsDeleted == 0)
        {
            Console.WriteLine("Product not found");
        }
        else
        {
            Console.WriteLine(rowsDeleted + " row(s) deleted.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//5.4
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string deleteQuery = "DELETE FROM Products WHERE ProductName = @name";

        SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
        deleteCmd.Parameters.AddWithValue("@name", "Backpack");

        deleteCmd.ExecuteNonQuery();

        string selectQuery = "SELECT COUNT(*) FROM Products WHERE ProductName = @name";

        SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
        selectCmd.Parameters.AddWithValue("@name", "Backpack");

        int count = (int)selectCmd.ExecuteScalar();

        if (count == 0)
        {
            Console.WriteLine("Product successfully removed");
        }
        else
        {
            Console.WriteLine("Product still exists");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//5.5
static bool DeleteProduct(SqlConnection conn, int productId)
{
    string query = "DELETE FROM Products WHERE ProductID = @id";

    SqlCommand command = new SqlCommand(query, conn);

    command.Parameters.AddWithValue("@id", productId);

    int rows = command.ExecuteNonQuery();

    return rows > 0;
}

// Call from Main()
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        bool result = DeleteProduct(connection, 5);

        if (result)
        {
            Console.WriteLine("Product deleted successfully");
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}


//6.1
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT COUNT(*) FROM Products";

        SqlCommand command = new SqlCommand(query, connection);

        int total = (int)command.ExecuteScalar();

        Console.WriteLine("Total products: " + total);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//6.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT MAX(Price) FROM Products";

        SqlCommand command = new SqlCommand(query, connection);

        decimal maxPrice = Convert.ToDecimal(command.ExecuteScalar());

        Console.WriteLine("Most expensive: " + maxPrice);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//6.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT MIN(Price) FROM Products";

        SqlCommand command = new SqlCommand(query, connection);

        decimal minPrice = Convert.ToDecimal(command.ExecuteScalar());

        Console.WriteLine("Cheapest product: " + minPrice);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//6.4

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT COUNT(*) FROM Products WHERE Category = @cat";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@cat", "Electronics");

        int count = (int)command.ExecuteScalar();

        Console.WriteLine("Electronics products: " + count);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//6.5

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = "SELECT AVG(Price) FROM Products";

        SqlCommand command = new SqlCommand(query, connection);

        decimal avgPrice = Convert.ToDecimal(command.ExecuteScalar());

        Console.WriteLine("Average price: " + Math.Round(avgPrice, 2));
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//6.6

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        string query = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                         VALUES (@name, @category, @price, @stock);
                         SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@name", "Bluetooth Speaker");
        command.Parameters.AddWithValue("@category", "Electronics");
        command.Parameters.AddWithValue("@price", 2999);
        command.Parameters.AddWithValue("@stock", 20);

        object result = command.ExecuteScalar();

        int newId = Convert.ToInt32(result);

        Console.WriteLine("New product created with ID = " + newId);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//7.1
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet ds = new DataSet();

        adapter.Fill(ds, "Products");

        int rowCount = ds.Tables["Products"].Rows.Count;

        Console.WriteLine("Total rows loaded: " + rowCount);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//7.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet ds = new DataSet();

        adapter.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine(
                "Name: " + row["ProductName"] +
                ", Category: " + row["Category"] +
                ", Price: " + row["Price"]);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//7.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet ds = new DataSet();

        adapter.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow[] rows = table.Select("Price > 1000");

        foreach (DataRow row in rows)
        {
            Console.WriteLine(
                "Name: " + row["ProductName"] +
                ", Price: " + row["Price"]);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//7.4
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet ds = new DataSet();

        adapter.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow[] rows = table.Select("", "Price DESC");

        Console.WriteLine("Top 3 Expensive Products:");

        for (int i = 0; i < Math.Min(3, rows.Length); i++)
        {
            Console.WriteLine(
                "Name: " + rows[i]["ProductName"] +
                ", Price: " + rows[i]["Price"]);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//7.5
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet ds = new DataSet();

        adapter.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow row = table.Rows[0];

        Console.WriteLine("Before change RowState: " + row.RowState);

        row["Price"] = 9999;

        Console.WriteLine("After change RowState: " + row.RowState);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//7.6
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        DataSet ds = new DataSet();

        adapter.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow[] rows = table.Select("Category = 'Stationery'");

        int totalStock = 0;

        foreach (DataRow row in rows)
        {
            Console.WriteLine(
                "Name: " + row["ProductName"] +
                ", Stock: " + row["Stock"]);

            totalStock += Convert.ToInt32(row["Stock"]);
        }

        Console.WriteLine("Total Stationery Stock: " + totalStock);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//8.1

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter da = new SqlDataAdapter(query, connection);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);

        DataSet ds = new DataSet();
        da.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow newRow = table.NewRow();
        newRow["ProductName"] = "Gaming Mouse";
        newRow["Category"] = "Electronics";
        newRow["Price"] = 2500;
        newRow["Stock"] = 20;

        table.Rows.Add(newRow);

        da.Update(ds, "Products");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//8.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter da = new SqlDataAdapter(query, connection);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);

        DataSet ds = new DataSet();
        da.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow[] rows = table.Select("ProductName = 'Gaming Mouse'");

        if (rows.Length > 0)
        {
            rows[0].BeginEdit();
            rows[0]["Price"] = 2999;
            rows[0].EndEdit();
        }

        da.Update(ds, "Products");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//8.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter da = new SqlDataAdapter(query, connection);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);

        DataSet ds = new DataSet();
        da.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow[] rows = table.Select("ProductName = 'Gaming Mouse'");

        if (rows.Length > 0)
        {
            rows[0].Delete();
        }

        da.Update(ds, "Products");

        ds.AcceptChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//8.4
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter da = new SqlDataAdapter(query, connection);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);

        DataSet ds = new DataSet();
        da.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        // Add
        DataRow newRow = table.NewRow();
        newRow["ProductName"] = "USB Cable";
        newRow["Category"] = "Accessories";
        newRow["Price"] = 199;
        newRow["Stock"] = 100;
        table.Rows.Add(newRow);

        // Update
        table.Rows[0]["Price"] = 7777;

        // Delete
        table.Rows[1].Delete();

        int rowsAffected = da.Update(ds, "Products");

        Console.WriteLine("Total rows affected: " + rowsAffected);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//8.5
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        string query = "SELECT * FROM Products";

        SqlDataAdapter da = new SqlDataAdapter(query, connection);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);

        DataSet ds = new DataSet();
        da.Fill(ds, "Products");

        DataTable table = ds.Tables["Products"];

        DataRow newRow = table.NewRow();
        newRow["ProductName"] = "Test Product";
        newRow["Category"] = "Test";
        newRow["Price"] = 100;
        newRow["Stock"] = 10;

        table.Rows.Add(newRow);

        Console.WriteLine("Before Update RowState: " + newRow.RowState);

        da.Update(ds, "Products");

        Console.WriteLine("After Update RowState: " + newRow.RowState);

    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

//9.1
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    SqlTransaction txn2 = connection.BeginTransaction();

    try
    {
        string query = @"INSERT INTO Products (ProductName, Category, Price, Stock)
                         VALUES (@name, 'Test', 100, 5)";

        SqlCommand cmd1 = new SqlCommand(query, connection, txn2);
        cmd1.Parameters.AddWithValue("@name", "Test Item");

        cmd1.ExecuteNonQuery();

        txn2.Commit();

        Console.WriteLine("Committed");
    }
    catch (Exception ex)
    {
        txn2.Rollback();
        Console.WriteLine("Error: " + ex.Message);
    }
}

//9.2
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    SqlTransaction txn3 = connection.BeginTransaction();

    try
    {
        // Insert
        SqlCommand insertCmd = new SqlCommand(
            "INSERT INTO Products (ProductName, Category, Price, Stock) VALUES ('Bundle Pack','Accessories',999,10)",
            connection, txn3);

        insertCmd.ExecuteNonQuery();

        // Update
        SqlCommand updateCmd = new SqlCommand(
            "UPDATE Products SET Stock = @stock WHERE ProductName = @name",
            connection, txn3);

        updateCmd.Parameters.AddWithValue("@stock", 5);
        updateCmd.Parameters.AddWithValue("@name", "Laptop");

        updateCmd.ExecuteNonQuery();

        txn3.Commit();

        Console.WriteLine("Transaction committed successfully");
    }
    catch (Exception ex)
    {
        txn3.Rollback();
        Console.WriteLine("Transaction rolled back: " + ex.Message);
    }
}

//9.3
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    SqlTransaction txn5 = connection.BeginTransaction();

    try
    {
        SqlCommand cmd = new SqlCommand(
            "UPDATE Products SET Price = 999 WHERE ProductName = 'Mouse'",
            connection, txn5);

        cmd.ExecuteNonQuery();

        txn5.Commit();

        Console.WriteLine("Transaction Committed");
    }
    catch (Exception ex)
    {
        txn5.Rollback();
        Console.WriteLine("Error: " + ex.Message);
    }
}

//9.4
static void TransferStock(string conString, int fromId, int toId, int qty)
{
    using (SqlConnection connection = new SqlConnection(conString))
    {
        connection.Open();

        SqlTransaction txn = connection.BeginTransaction();

        try
        {
            // Deduct stock from source product
            SqlCommand deductCmd = new SqlCommand(
                "UPDATE Products SET Stock = Stock - @qty WHERE ProductID = @id",
                connection, txn);

            deductCmd.Parameters.AddWithValue("@qty", qty);
            deductCmd.Parameters.AddWithValue("@id", fromId);

            deductCmd.ExecuteNonQuery();

            // Add stock to destination product
            SqlCommand addCmd = new SqlCommand(
                "UPDATE Products SET Stock = Stock + @qty WHERE ProductID = @id",
                connection, txn);

            addCmd.Parameters.AddWithValue("@qty", qty);
            addCmd.Parameters.AddWithValue("@id", toId);

            addCmd.ExecuteNonQuery();

            txn.Commit();

            Console.WriteLine("Stock transferred successfully");
        }
        catch (Exception ex)
        {
            txn.Rollback();
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}

//9.5
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        Console.WriteLine("Before Transaction:");
        SqlCommand beforeCmd = new SqlCommand("SELECT COUNT(*) FROM Products", connection);
        Console.WriteLine("Total: " + beforeCmd.ExecuteScalar());

        SqlTransaction txn4 = connection.BeginTransaction();

        try
        {
            SqlCommand cmd2 = new SqlCommand(
                "INSERT INTO Products (ProductName, Category, Price, Stock) VALUES ('FailTest','Test',100,10)",
                connection, txn4);

            beforeCmd.ExecuteNonQuery();

            // Force failure
            throw new Exception("Simulated failure");

            txn4.Commit();
        }
        catch
        {
            txn4.Rollback();
            Console.WriteLine("Transaction rolled back");
        }

        Console.WriteLine("After Transaction:");
        SqlCommand afterCmd = new SqlCommand("SELECT COUNT(*) FROM Products", connection);
        Console.WriteLine("Total: " + afterCmd.ExecuteScalar());
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}