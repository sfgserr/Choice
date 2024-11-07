using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetCategories
{
    internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetCategoriesQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }
        
        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery query)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                SELECT 
                    administration."Categories"."Id" as {nameof(CategoryDto.CategoryId)},
                    administration."Categories"."Title" as {nameof(CategoryDto.Title)},
                    administration."Categories"."IconUri" as {nameof(CategoryDto.IconUri)}
                FROM administration."Categories"
                """;

            return await connection.QueryAsync<CategoryDto>(sql);
        }
    }
}