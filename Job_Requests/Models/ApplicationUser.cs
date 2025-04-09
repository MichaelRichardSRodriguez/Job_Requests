using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Requests.Models
{
	public class ApplicationUser: IdentityUser
	{

		[DisplayName("First Name")]
		[StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
		[Required(ErrorMessage = "First Name is Required.")]
		public string FirstName { get; set; }

		[DisplayName("Middle Name")]
		[StringLength(50, ErrorMessage = "Middle Name cannot be longer than 50 characters.")]
		public string MiddleName { get; set; }

		[DisplayName("Last Name")]
		[StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
		[Required(ErrorMessage = "Last Name is Required.")]
		public string LastName { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

		[ForeignKey("DepartmentId")]
		[ValidateNever]
		public Department Department { get; set; }

		[NotMapped]
		public string Role { get; set; }

        //public string FullName { get; set; }
    }
}
