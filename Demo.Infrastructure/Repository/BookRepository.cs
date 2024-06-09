using System.Data;
using System.Data.SqlClient;
using Dapper;
using Demo.Application.Interfaces;
using Demo.Core.Entities;
using Demo.Sql.Queries;
using Microsoft.Extensions.Configuration;

namespace Demo.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        #region ===[ Private Members ]=============================================================
        private readonly IConfiguration configuration;
        #endregion

        #region ===[ Constructor ]=================================================================
        public BookRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region ===[ IBookRepository Methods ]==================================================

        public async Task<IReadOnlyList<Book>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Book>(BookQueries.AllBook);
                return result.ToList();
            }
        }

        public async Task<Book> GetByIdAsync(long id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Book>(BookQueries.BookById, new { BookId = id });
                return result;
            }
        }

        public async Task<string> AddAsync(Book entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(BookQueries.AddBook, entity);
                return result.ToString();
            }
        }

        public async Task<string> UpdateAsync(Book entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(BookQueries.UpdateBook, entity);
                return result.ToString();
            }
        }

        public async Task<string> DeleteAsync(long id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(BookQueries.DeleteBook, new { BookId = id });
                return result.ToString();
            }
        }

        public async Task<PagedResults<Book>> GetBooksAsync(GetBooksQuery requestParmater)
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                await conn.OpenAsync();

                // Set first query
                var statement = $"WHERE json_value([BookInfo],'$.BookTitle')='{requestParmater.BookTitle}' OR json_value([BookInfo],'$.BookDescription')='{requestParmater.BookDescription}' OR json_value([BookInfo],'$.Author')='{requestParmater.Author}' OR json_value([BookInfo],'$.PublishDate')='{requestParmater.PublishDate}'"; 
                var whereStatement = string.IsNullOrWhiteSpace(requestParmater.BookTitle) && string.IsNullOrWhiteSpace(requestParmater.BookDescription) && string.IsNullOrWhiteSpace(requestParmater.Author) &&  string.IsNullOrWhiteSpace(requestParmater.PublishDate) ? "" : statement;
                var queries = @$"
               {BookQueries.AllBook}
                {whereStatement}
                ORDER BY [BookId]
                OFFSET @PageSize * (@PageNumber - 1) ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

                // Set second query, separated with semi-colon
                queries += "SELECT COUNT(*) AS TotalItems FROM [Book] (NOLOCK);";

                // Execute multiple queries with Dapper in just one step
                using var multi = await conn.QueryMultipleAsync(queries,
                    new
                    {
                        PageNumber = requestParmater.PageNumber,
                        PageSize = requestParmater.PageSize
                    });

                // Fetch Items by OFFSET-FETCH clause
                var items = await multi.ReadAsync<Book>().ConfigureAwait(false);

                // Fetch Total items count
                var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);

                // Create paged result
                var result = new PagedResults<Book>(totalItems, requestParmater.PageNumber, requestParmater.PageSize)
                {
                    Items = items
                };
                return result;
            }
        }
     
    }
    #endregion
}
