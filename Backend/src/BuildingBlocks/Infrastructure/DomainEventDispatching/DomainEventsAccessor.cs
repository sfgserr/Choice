using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching
{
    public class DomainEventsAccessor
    {
        private readonly DbContext _dbContext;

        public DomainEventsAccessor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            var entities = GetEntities();

            return entities.SelectMany(e => e.DomainEvents).ToList();
        }

        public void ClearAllDomainEvents()
        {
            var entities = GetEntities();

            foreach (Entity entity in entities)
                entity.ClearEvents();
        }

        private IEnumerable<Entity> GetEntities()
        {
            return _dbContext.ChangeTracker.Entries<Entity>()
               .Where(e => e.Entity.DomainEvents.Count != 0)
               .Select(e => e.Entity);
        }
    }
}
