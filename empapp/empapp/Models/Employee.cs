using MongoDB.Bson.Serialization.Attributes;

public class Employee
{
    [BsonId]
    public int Id { get; set; }
    [BsonElement{"name"}]
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
}
