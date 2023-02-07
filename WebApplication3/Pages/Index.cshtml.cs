using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor contxt;
        private readonly SignInManager<ApplicationUser> signInManager;

        public IndexModel(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            contxt = httpContextAccessor;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }
    }
}