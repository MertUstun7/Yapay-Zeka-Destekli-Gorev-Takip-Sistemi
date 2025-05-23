using Entities.Models;

public class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string TaxNumber { get; set; }

    // Şirket sahibi
    public string OwnerId { get; set; }
    public User Owner { get; set; }

    // Şirkete bağlı kullanıcılar
    public ICollection<User> Users { get; set; } = new List<User>();

    // Şirkete ait görevler
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
