using Choice.CategoryService.Api.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Choice.CategoryService.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Add(Category category)
        {
            using NpgsqlConnection connection = new(_configuration["PostgreSqlSettings:ConnectionString"]);

            await connection.ExecuteAsync
                ("INSERT INTO Categories (Title, IconUri) VALUES (@title, @iconUri)", 
                    new { title = category.Title, iconUri = category.IconUri });
        }

        public async Task<Category> Get(int id)
        {
            using NpgsqlConnection connection = new(_configuration["PostgreSqlSettings:ConnectionString"]);

            return await connection.QueryFirstAsync<Category>
                ("SELECT * FROM Categories WHERE Id=@id", new { id = id });
        }

        public async Task<IList<Category>> GetAll()
        {
            using NpgsqlConnection connection = new(_configuration["PostgreSqlSettings:ConnectionString"]);

            var categories = await connection.QueryAsync<Category>("SELECT * FROM Categories");

            return categories.ToList();
        }

        public async Task<bool> Update(Category category)
        {
            using NpgsqlConnection connection = new(_configuration["PostgreSqlSettings:ConnectionString"]);

            int affections = await connection.ExecuteAsync
                ("UPDATE Categories SET Title = @title, IconUri = @iconUri WHERE Id = @id", 
                    new { title = category.Title, iconUri = category.IconUri, id = category.Id });

            return affections > 0;
        }

        public async Task Delete(int id)
        {
            using NpgsqlConnection connection = new(_configuration["PostgreSqlSettings:ConnectionString"]);

            await connection.ExecuteAsync("DELETE FROM Categories WHERE Id = @id", new { id = id });
        }
    }
}
