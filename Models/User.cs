using System;
using System.ComponentModel.DataAnnotations;

/*
* Data annotations for tell EntityFramework
* how to treat the fields of each model.
*/
namespace DotNetAPI.Models
{
    public class User
    {
        [Key] // Primary Key attribute
        public int Id { get; set; }

        [Required] // Required field attribute
        [MaxLength(20)] // heavily used for validation and will restrict strings
        public string Name { get; set; }

        [DataType(DataType.PostalCode)] // Data Type attribute, extensively used by ASP.NET MVC for generating form specific elements
        public string OriginPostCode { get; set; }

        public GenderClass Gender { get; set; }
    }

    public enum GenderClass
    {
        Male,
        Female,
        Other
    }
}
