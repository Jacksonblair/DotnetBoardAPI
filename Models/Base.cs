using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class BaseDataModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Let the database generate the value
    public string Id { get; set; }
}

public interface IEntity
{
    string Id { get; set; }
}

public interface IItemHandler {
    Task Handle(Item item, ApplicationDbContext dbContext);
}