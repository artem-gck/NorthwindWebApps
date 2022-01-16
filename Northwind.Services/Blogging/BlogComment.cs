using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Blogging
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string? Comment { get; set; }
    }
}
