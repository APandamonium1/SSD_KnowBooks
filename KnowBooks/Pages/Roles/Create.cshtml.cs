using KnowBooks.Data;
using KnowBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;
namespace KnowBooks.Pages.Roles
{
	public class CreateModel : PageModel
	{
		private readonly RoleManager<ApplicationRole> _roleManager;


		public CreateModel(RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public ApplicationRole ApplicationRole { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{

			if (!ModelState.IsValid)
			{
				return Page();
			}


			ApplicationRole.CreatedDate = DateTime.UtcNow;
					
			IdentityResult roleRuslt = await _roleManager.CreateAsync(ApplicationRole);
			
			if (roleRuslt.Succeeded)
			{
				// Role creation succeeded
				return RedirectToPage("Index");
			}
			else
			{
				//Role creation failed, handle the error(e.g., display error message)
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					Console.WriteLine(error.ErrorMessage);
				}
				return Page();
	
			}

		}

	}
}
