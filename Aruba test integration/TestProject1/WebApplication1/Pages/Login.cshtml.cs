using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1;

namespace WebApplication1.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public LoginModel(IConfiguration conf)
        {
            _configuration = conf;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var temp = new TokenUtils(_configuration);
            var temp2 = temp.GenerateToken(Username);
            Response.Cookies.Append("AuthToken", temp2, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1) 
            });
            return RedirectToPage("/Index");
        }
    }
}
