using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class CategoryItemHandler : IItemHandler
{
    public Task Handle(Item item, ApplicationDbContext dbContext)
    {
        throw new NotImplementedException();
    }
}