using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KnowBooks.Models
{
    public class ApplicationRole : IdentityRole
    {
		[Required]
		public override string Name { get; set; }

		public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }
}