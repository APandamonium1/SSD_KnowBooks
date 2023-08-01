using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Markup;

namespace KnowBooks.Models
{
    public class Book
    {

        [Key] //Sets ISBN as primary key with PK constraints
        public int ISBN { get; set; }

        [Required, StringLength(60, MinimumLength = 2)] 
        //Input validation, ensures that the title is not too long (>60 characters) or too short (<2 characters)
        //Indicates Title as mandatory field`
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(60)] //Max 60 characters
        //Indicates Author as mandatory field
        public string Author { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Please ensure that input matches the regular expression ^[A-Z]+[a-zA-Z\\s]*$ and capitalise first letter.")] 
        //Genre should only have RegEx characters
        [Required, StringLength(30)] //Max 30 characters
        //Indicates Genre as mandatory field
        public string Genre { get; set; } = string.Empty;

        [Required]
        private string? _availabilityStatus;
        //Displays as 2 words instead of AvailabilityStatus
        [Display(Name = "Availability Status")]
        [RegularExpression(@"^[A - Za - z]{1, 20}$", ErrorMessage = "Please enter either 'Available' or 'Loaned'.")]
        //Following code to restrict the input for Availability Status to only 2 string
        //values ("Available", "Loaned")
        public string AvailabilityStatus
        {
            get { return _availabilityStatus; }
            set {}
        }
    }
}