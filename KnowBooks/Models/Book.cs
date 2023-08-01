using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowBooks.Models
{
    public class Book
    {
        [Key] //Sets ISBN as primary key with PK constraints
        public int ISBN { get; set; }
        [StringLength(60, MinimumLength = 3)] 
        //Input validation, ensures that the title is not too long (>60 characters)
        //or too short (<30 characters)
        [Required] //Indicates Title as mandatory field`
        public string Title { get; set; } = string.Empty;
        [Required] //Indicates Author as mandatory field
        [StringLength(60)] //Max 60 characters
        public string Author { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")] 
        //Genre should only have RegEx characters
        [Required] //Indicates Genre as mandatory field
        [StringLength(30)] //Max 30 characters
        public string Genre { get; set; } = string.Empty;
        private string? _availabilityStatus;
        [Display(Name = "Available Status")]//Displays as 2 words instead of AvailabilityStatus
        //Following code to restrict the input for Availability Status to only 3 string
        //values ("Available", "Reserved" or "Loaned")
        public string AvailabilityStatus 
        {
            get { return _availabilityStatus; }
            set
            {
                // Checking if the value is one of the three allowed values
                if (value == "Available" || value == "Reserved" || value == "Loaned")
                {
                    _availabilityStatus = value;
                }
                else
                {
                    throw new ArgumentException("Invalid availability status. Allowed values are Available, Reserved, and Loaned.");
                }
            }
        }
    }
}
