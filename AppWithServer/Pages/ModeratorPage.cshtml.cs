using AppWithServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppWithServer.Pages
{
    public class ModeratorPageModel : PageModel
    {
        private readonly Srs2Context _context;

        public ModeratorPageModel(Srs2Context context)
        {
            _context = context;
        }

        public IList<User> Users { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Good> Goods { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
            Orders = await _context.Orders.Include(o => o.User).Include(o => o.Good).ToListAsync();
            Goods = await _context.Goods.ToListAsync();
        }

        // �������������� ������������
        public async Task<IActionResult> OnPostEditUserAsync(string login, string password, bool role, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Login = login;
                user.Password = password;
                user.Role = role;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������������� ������
        public async Task<IActionResult> OnPostEditOrderAsync(int count, int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Count = count;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������������� ������
        public async Task<IActionResult> OnPostEditGoodsAsync(string name, string description, bool inStock, int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods != null)
            {
                goods.Name = name;
                goods.Discription = description;
                goods.InStock = inStock;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������� ������������
        public async Task<IActionResult> OnPostDeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������� ������
        public async Task<IActionResult> OnPostDeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // �������� ������
        public async Task<IActionResult> OnPostDeleteGoodsAsync(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods != null)
            {
                _context.Goods.Remove(goods);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
