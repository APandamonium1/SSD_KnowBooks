using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowBooks.Models
{
    public class Book
    {
        [Key] //Sets ISBN as primary key with PK constraints
        public int ISBN { get; set; }


        [Required, StringLength(60, MinimumLength = 3)]
        //Input validation, ensures that the title is not too long (>60 characters) or too short (<30 characters)
        //Indicates Title as mandatory field
        public string Title { get; set; } = string.Empty;


        [Required, StringLength(60)] //Max 60 characters
        //Indicates Author as mandatory field
        public string Author { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Please match regular expression ^[A-Z]+[a-zA-Z\\s]* and capitalise first letter.")]
        //Genre should only have RegEx characters
        [Required, StringLength(30)] //Max 30 characters
        //Indicates Genre as mandatory field
        public string Genre { get; set; } = string.Empty;


        //[RegularExpression("^[A-Za-z]{1,20}$", ErrorMessage = "Please enter either 'Available' or 'Loaned' only.")] //RegEx only, capitalise 1st letter
        [Required, StringLength(9, MinimumLength =6, ErrorMessage = "Please enter either 'Available' or 'Loaned' only.")]
        [Display(Name = "Available Status")]//Displays as 2 words instead of AvailabilityStatus
        //Following code to restrict the input for Availability Status to only 2 string values ("Available" or "Loaned")
        public string AvailabilityStatus
        {
            get { return _availabilityStatus; }
            set 
            {
                // Set the default value of the AvailabilityStatus property to Available
                //_availabilityStatus = "Available";
            }
        }*/


        [Required, StringLength(60)] //Max 60 characters
        //Indicates Author as mandatory field
        public string Borrower { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
