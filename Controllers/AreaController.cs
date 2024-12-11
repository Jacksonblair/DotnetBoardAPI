using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Project/5/Areas
        [HttpGet("Projects/{projectId}/Areas")]
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAreas(string projectId)
        {
            var project = await _context.Projects.Include(p => p.Areas).FirstOrDefaultAsync(p => p.Id == projectId);
            
            if (project == null)
            {
                return NotFound();
            }
            
            return AreaService.ToAreaDtos(project.Areas);
        }

        // GET: api/Project/5/Areas/5
        [HttpGet("Projects/{projectId}/Areas/{areaId}")]
        public async Task<ActionResult<AreaDto>> GetArea(string projectId, string areaId)
        {
            var project = await _context.Projects.Include(p => p.Areas).FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }
            
            var area = project.Areas.FirstOrDefault(a => a.Id == areaId);

            if (area == null)
            {
                return NotFound();
            }

            return AreaService.ToAreaDto(area);
        }

        // PUT: api/Project/5/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Projects/{projectId}/Areas")]
        public async Task<IActionResult> PutArea(string projectId, UpdateAreaDto updateAreaDto)
        {
            var project = await _context.Projects.Include(p => p.Areas).FirstOrDefaultAsync(p => p.Id == projectId);
            
            if (project == null)
            {
                return NotFound();
            }

            // If area exists, update existing area
            if (updateAreaDto.Id != null)
            {
                var area = project.Areas.FirstOrDefault(a => a.Id == updateAreaDto.Id);
                if (area != null)
                {
                    area.Name = updateAreaDto.Name;
                }
            }
            else
            {
                // If area does not exist, create a new one and add it to the project
                var area = AreaService.CreateArea(updateAreaDto);
                project.Areas.Add(area);
                _context.Areas.Add(area);
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();  // Successfully updated or added the area
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Project/5/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Projects/{projectId}/Areas")]
        public async Task<ActionResult<AreaDto>> PostArea(string projectId, CreateAreaDto createAreaDto)
        {
            var project = await _context.Projects.Include(p => p.Areas).FirstOrDefaultAsync(p => p.Id == projectId);
            
            if (project == null)
            {
                return NotFound();
            }
            
            var newArea = AreaService.CreateArea(createAreaDto);
            
            project.Areas.Add(newArea);
            _context.Areas.Add(newArea);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetArea), new { projectId, areaId = newArea.Id }, AreaService.ToAreaDto(newArea));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Project/5/Areas/5
        [HttpDelete("/Project/{projectId}/Areas/{areaId}")]
        public async Task<IActionResult> DeleteArea(string projectId, string areaId)
        {
            var project = await _context.Projects.Include(p => p.Areas).FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                return NotFound();
            }
            
            var area = project.Areas.FirstOrDefault(a => a.Id == areaId);
            if (area == null)
            {               
                return NotFound();
            }
            
            // Remove the area from the project's Areas collection
            project.Areas.Remove(area);
            _context.Areas.Remove(area);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();  // Return 204 No Content to indicate successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
