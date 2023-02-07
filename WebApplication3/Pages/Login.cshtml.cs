using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using WebApplication3.Model;
using AspNetCore.ReCaptcha;
using ApplicationSecurity.Captcha;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly GoogleCaptchaService _captchaService;
        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, GoogleCaptchaService service)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _captchaService = service;
        }

        public void OnGet()
        {
        }

        [ValidateReCaptcha]
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            //Verify Response Token With Google
            var captchaResult = await _captchaService.VerifyToken(LModel.Token);
            if (!captchaResult)
            {
                return Page();
            }

            var result = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
            if (ModelState.IsValid)
            {
                if(result.IsLockedOut)
                {
                    ModelState.AddModelError("", "The account is locked out");
                    return Page();
                }
                else
                {
                    var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                    LModel.RememberMe, false);
                    if (identityResult.Succeeded)
                    {
                        HttpContext.Session.Clear();
                        HttpContext.Session.SetString("username", LModel.Email);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password incorrect");
                    }
                }
            }
            return Page();
        }
    }
}
