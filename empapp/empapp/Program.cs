using MongoDB.Driver;

var conString = "mongodb://localhost:27017";
MongoClient client = new MongoClient(conString);
var database = client.GetDatabase("demodb");
var employees = database.GetCollection<Employee>("employees");
var emp1= new Employee { Id = 1, Name = "Alice", Position = "Developer", Salary = 60000 };
employees.InsertOne(emp1);
Console.WriteLine("Employee inserted successfully.");