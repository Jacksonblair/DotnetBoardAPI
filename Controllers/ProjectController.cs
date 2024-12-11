using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectController> _logger;

        
        public ProjectController(ApplicationDbContext context, ILogger<ProjectController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return ProjectService.ToProjectDtos(projects);
        }

        // GET: api/Project/5
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProject(string projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            return ProjectService.ToProjectDto(project);
        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProject(UpdateProjectDto updateProjectDto)
        {
            Console.WriteLine(updateProjectDto.ToJson());
            
            try
            {
                if (updateProjectDto.Id != null)
                {
                    var project = await _context.Projects.FindAsync(updateProjectDto.Id);
                    if (project == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        project.Name = updateProjectDto.Name;
                        
                        // Mark entity as modified
                        _context.Entry(project).State = EntityState.Modified;
                    }
                }
                else
                {
                    // If area does not exist, create a new one and add it to the project
                    var project = ProjectService.CreateProject(updateProjectDto);
                    Console.WriteLine(project.ToJson());
                    _context.Projects.Add(project);
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> PostProject(CreateProjectDto createProjectDto)
        {
            var project = ProjectService.CreateProject(createProjectDto);
            _context.Projects.Add(project);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            Console.WriteLine(project);
            return CreatedAtAction("GetProject", new { projectId = project.Id }, createProjectDto);
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}