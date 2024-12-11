using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public enum ItemType
{
    BooleanItem,
    CategoryItem
}

public class Item : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public Area Area { get; set; }
    public string AreaId { get; set; }
    ItemType ItemType { get; }
    string? CategoryId { get; set; }
    string? Value { get; set; }
}

public class ItemDto
{
    public string Id { get; set; }
    public bool IsRootArea { get; set; }
}

public class CreateItemDto
{
    ItemType ItemType { get; }
    string? CategoryId { get; set; }
    string? Value { get; set; }
}

public class UpdateItemDto
{
    string? CategoryId { get; set; }
    string? Value { get; set; }
};