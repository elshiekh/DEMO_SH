using Demo.Core.Entities;

namespace Demo.Api.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public PagedResults<Book>? QueryResult { get; set; }
    }
}
