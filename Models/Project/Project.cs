using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

/** Projects contain areas, which contain Items */
public class Project : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<Area> Areas { get; set; }
}


public class ProjectDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class CreateProjectDto
{
    public string Name { get; set; }
}

public class UpdateProjectDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
}

public class ProjectService
{
    public static Project CreateProject(CreateProjectDto createProjectDto)
    {
        return new Project
        {
            Name = createProjectDto.Name
        };
    }

    public static Project CreateProject(UpdateProjectDto updateProjectDto)
    {
        return new Project
        {
            Name = updateProjectDto.Name
        };
    }

    public static ProjectDto ToProjectDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name
        };
    }
    
    // Process an array (or list) of CreateProjectDto and create multiple projects
    public static List<Project> CreateProjects(IEnumerable<CreateProjectDto> createProjectDtos)
    {
        return createProjectDtos.Select(ProjectService.CreateProject).ToList();
    }

    // Convert an array (or list) of Project to an array (or list) of ProjectDto
    public static List<ProjectDto> ToProjectDtos(IEnumerable<Project> areas)
    {
        return areas.Select(ProjectService.ToProjectDto).ToList();
    }
}