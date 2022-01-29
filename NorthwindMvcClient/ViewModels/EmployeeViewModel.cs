using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace NorthwindMvcClient.ViewModels
{
    public class EmployeeViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DisplayName("Last name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DisplayName("First name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the title of courtesy.
        /// </summary>
        /// <value>
        /// The title of courtesy.
        /// </value>
        [DisplayName("Title Of Courtesy")]
        public string? TitleOfCourtesy { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the hire date.
        /// </summary>
        /// <value>
        /// The hire date.
        /// </value>
        [DisplayName("Hire Date")]
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [DisplayName("Postal Code")]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        /// <value>
        /// The home phone.
        /// </value>
        [DisplayName("Postal Code")]
        public string? HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string? Extension { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public IFormFile? Photo { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the reports to.
        /// </summary>
        /// <value>
        /// The reports to.
        /// </value> 
        [DisplayName("Reports To")]
        public int? ReportsTo { get; set; }

        /// <summary>
        /// Gets or sets the photo path.
        /// </summary>
        /// <value>
        /// The photo path.
        /// </value>
        [DisplayName("Photo Path")]
        public string? PhotoPath { get; set; }
    }
}
