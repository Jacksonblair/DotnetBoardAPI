using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

/** Areas are a sub-section of Projects */
public class Area : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public bool IsRootArea { get; set; }
    public string Name { get; set; }
    public string ProjectId { get; set; }
    public Project Project { get; set; }
    public ICollection<Item> Items { get; set; }
}

public class AreaDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsRootArea { get; set; }
}

public class CreateAreaDto
{
    public string Name { get; set; }
    public bool IsRootArea { get; set; }
}

public class UpdateAreaDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public bool IsRootArea { get; set; }
}

public class AreaService
{
    public static Area CreateArea(CreateAreaDto createAreaDto)
    {
        return new Area
        {
            Name = createAreaDto.Name,
            IsRootArea = createAreaDto.IsRootArea
        };
    }

    public static Area CreateArea(UpdateAreaDto updateAreaDto)
    {
        return new Area
        {
            IsRootArea = updateAreaDto.IsRootArea
        };
    }

    public static AreaDto ToAreaDto(Area area)
    {
        return new AreaDto
        {
            Id = area.Id,
            IsRootArea = area.IsRootArea,
            Name = area.Name
        };
    }
    
    // Process an array (or list) of CreateAreaDto and create multiple areas
    public static List<Area> CreateAreas(IEnumerable<CreateAreaDto> createAreaDtos)
    {
        return createAreaDtos.Select(AreaService.CreateArea).ToList();
    }

    // Convert an array (or list) of Area to an array (or list) of AreaDto
    public static List<AreaDto> ToAreaDtos(IEnumerable<Area> areas)
    {
        return areas.Select(AreaService.ToAreaDto).ToList();
    }
}