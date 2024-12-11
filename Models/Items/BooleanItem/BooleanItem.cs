using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class BooleanItemHandler : IItemHandler
{
    public Task Handle(Item item, ApplicationDbContext context)
    {
        throw new NotImplementedException();
    }
}