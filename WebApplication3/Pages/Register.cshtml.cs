using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        // Initialize the built-in ASP.NET Identity
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private IWebHostEnvironment _environment;

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
        }


        public void OnGet()
        {
        }


        // Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var imageFile = "";
                var imagePath = "";

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var Photo = "bruh";

                if (RModel.Photo != null)
                {
                    if (RModel.Photo.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    imageFile = Guid.NewGuid() + Path.GetExtension(RModel.Photo.FileName);
                    imagePath = Path.Combine(_environment.ContentRootPath,"wwwroot", uploadsFolder, imageFile);
                    Photo = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                    var checktype = Photo.Substring(Photo.Length - 3);
                    if (checktype != "jpg" && checktype !="JPG")
                    {
                        ModelState.AddModelError("Upload", $"Only .JPG files accepted");
                        return Page();
                    }

                }

                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FullName = RModel.FullName,
                    CreditCardNo = protector.Protect(RModel.CreditCardNo),
                    Gender = RModel.Gender,
                    MobileNumber = RModel.MobileNumber,
                    Address = RModel.Address,
                    AboutMe = RModel.AboutMe,
                    Photo = Photo
            };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    
                    //Save photo into uploads folder
                    using var fileStream = new FileStream(imagePath,FileMode.Create);
                    await RModel.Photo.CopyToAsync(fileStream);

                    // Create session for user
                    HttpContext.Session.SetString("username", RModel.Email);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}
