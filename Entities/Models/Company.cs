using Entities.Models;

public class Company
{
    // Veri tabanında şirket ID' sini temsil eden parametremdir.
    public Guid Id { get; set; } = Guid.NewGuid();

    // Veri tabanında şirket ismini temsil eden parametremdir.
    public string Name { get; set; }

    // Veri tabanında şirketin vergi numarasını temsil eden parametremdir.
    public string TaxNumber { get; set; }

    // Veri tabanında şirket'in sahibinin ID numarasını temsil eden parametremdir.
    public string OwnerId { get; set; }

    // Şirket sahibi olan kullanıcıyla olan ilişkiyi tanımlar
    public User Owner { get; set; }

    // Şirket bünyesinde bulunan kullanıcıları temsil eden koleksiyondur.
    public ICollection<User> Users { get; set; } = new List<User>();

    // Şirket bünyesinde bulunan görevleri temsil eden koleksiyondur.
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}


