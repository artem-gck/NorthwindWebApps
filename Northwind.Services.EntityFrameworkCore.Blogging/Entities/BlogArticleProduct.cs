using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Entities
{
    /// <summary>
    /// BlogArticleProduct class.
    /// </summary>
    [Index(nameof(BlogArticleID), Name = "BlogArticleID")]
    public class BlogArticleProduct
    {
        /// <summary>
        /// Gets or sets the blog article product identifier.
        /// </summary>
        /// <value>
        /// The blog article product identifier.
        /// </value>
        [Key]
        [Column("BlogArticleProductID")]
        public int BlogArticleProductID { get; set; }

        /// <summary>
        /// Gets or sets the blog article identifier.
        /// </summary>
        /// <value>
        /// The blog article identifier.
        /// </value>
        public int BlogArticleID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductID { get; set; }
    }
}
