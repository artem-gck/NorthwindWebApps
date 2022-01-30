using Northwind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Blogging
{
    public class BlogArticleProductShow
    {
        public string BlogArticleName { get; set; }

        public Product[] Products { get; set; }
    }
}
