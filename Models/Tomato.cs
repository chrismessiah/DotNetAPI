using System;
using System.ComponentModel.DataAnnotations;

/*
* Data annotations for tell EntityFramework
* how to treat the fields of each model.
*/
namespace TomatoAPI.Models
{
    public class Tomato
    {
        [Key] // Primary Key attribute
        public int Id { get; set; }

        [Required] // Required field attribute
        [MaxLength(20)] // heavily used for validation and will restrict strings
        public string Name { get; set; }

        [DataType(DataType.PostalCode)] // Data Type attribute, extensively used by ASP.NET MVC for generating form specific elements
        public string OriginPostCode { get; set; }

        public TasteRating Tastes { get; set; }
    }

    public enum TasteRating
    {
        Terrible,
        Eh,
        Good,
        Great,
        Superb
    }
}
