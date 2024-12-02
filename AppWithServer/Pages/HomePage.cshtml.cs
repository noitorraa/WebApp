using AppWithServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppWithServer.Pages
{
    public class HomePageModel : PageModel
    {
        private readonly Srs2Context _context;

        public HomePageModel(Srs2Context context)
        {
            _context = context;
        }

        public IList<Good> Goods { get; set; }
        public IList<Order> Orders { get; set; }

        // ����� ��� �������� ������� � ������� ������������
        public async Task OnGetAsync()
        {
            Goods = await _context.Goods.ToListAsync();

            // �������� UserId �� ������
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
               // ��������� ������ ��� ����� ������������
               Orders = await _context.Orders
                   .Where(o => o.UserId == userId.Value)
                   .Include(o => o.Good) // � o.Good ��������� ������, ��� ��� order �� �������� good
                   .ToListAsync();
            }
        }

        // ����� ��� �������� ������
        public async Task<IActionResult> OnPostCreateOrderAsync(int goodsId, int quantity)
        {
            // �������� UserId �� ������
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // ���������, ���������� �� ����� � ���� �� �� � �������
                var goods = await _context.Goods.FindAsync(goodsId);
                if (goods != null && goods.InStock && quantity > 0)
                {
                    // ������� �����
                    var order = new Order
                    {
                        UserId = userId.Value,  // ���������� UserId ��� int
                        GoodsId = goods.Id,
                        Count = quantity
                    };

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "����� ������� ��������!";
                    return RedirectToPage();
                }

                // ���� ����� �� ������ ��� ��� ��� � ������� (��� �������)
                TempData["Error"] = "������ ��� �������� ������.";
                return RedirectToPage();
            }

            // ���� UserId �� ������ � ������ (��� �������)
            TempData["Error"] = "������������ �� ������.";
            return RedirectToPage();
        }
    }
}
