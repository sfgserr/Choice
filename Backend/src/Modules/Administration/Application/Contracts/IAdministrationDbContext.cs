using Administration.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Administration.Application.Contracts
{
    public interface IAdministrationDbContext
    {
        DbSet<Category> Categories { get; }
    }
}