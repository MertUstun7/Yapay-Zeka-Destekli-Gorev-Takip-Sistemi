using Entities.Models;

public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string TaxNumber { get; set; }
    public string OwnerId { get; set; }
    public User Owner { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
