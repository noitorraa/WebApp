using AppWithServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWithServer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Srs2Context _context;

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public IndexModel(ILogger<IndexModel> logger, Srs2Context context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (user != null)
            {
                if (user.Role == true)
                {
                    Console.WriteLine($"UserId �� TempData: {TempData["UserId"]}");
                    HttpContext.Session.SetInt32("UserId", user.IdUser);
                    return RedirectToPage("/HomePage");  // ��������� �� ������� ��������
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", user.IdUser);
                    return RedirectToPage("/ModeratorPage");  // ��������� �� �������� ����������
                }
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "������������ ����� ��� ������.");
                return Page();
            }
        }
    }
}
