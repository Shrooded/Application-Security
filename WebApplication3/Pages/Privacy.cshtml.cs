using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    [Authorize]
    public class PrivacyModel : PageModel
    {
        private readonly IHttpContextAccessor contxt;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger, IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            this.signInManager = signInManager;
            contxt = httpContextAccessor;
        }

        public void OnGet()
        {
        }
    }
}