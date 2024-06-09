using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Entities
{
    public class GetBooksQuery
    {
        public string? BookTitle { get; set; }
        public string? BookDescription { get; set; }
        public string? Author { get; set; }
        public string? PublishDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
