using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Model
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public string CreditCardNo { get; set; }
		public string Gender { get;set; }
		public string MobileNumber { get; set; }
		public string Address { get; set; }
        public string Email { get; set; }
		public string AboutMe { get; set; }
		public string Photo { get; set; }
	}
}
