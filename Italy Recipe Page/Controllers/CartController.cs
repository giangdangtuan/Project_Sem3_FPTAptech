using Italy_Recipe_Page.Models;
using Italy_Recipe_Page.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Italy_Recipe_Page.Helper;
using Microsoft.AspNetCore.Authorization;
using Italy_Recipe_Page.Helper;
using Italy_Recipe_Page.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Italy_Recipe_Page.Controllers
{
    public class CartController : Controller
    {
        private readonly ItalyContext db;
		private readonly IVnPayService _vnPayservice;
		public CartController(ItalyContext context, IVnPayService vnPayservice)
        {
            db = context;
			_vnPayservice = vnPayservice;
		}

        const string CART_KEY = "MYCART";
		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

		public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
				List<CartItem> idlist = new List<CartItem>();
				var gioHang = Cart;
                
				var item = gioHang.SingleOrDefault(p => p.PackId == id);
			if (item == null)
			{
				var hangHoa = db.Packs.Where(u => u.PackId == id).FirstOrDefault();
				if (hangHoa == null)
				{
					TempData["Message"] = $"Cant found {id}";
					return Redirect("/404");
				}
				item = new CartItem
				{
					PackId = hangHoa.PackId,
					PackName = hangHoa.PackName,
					Pack_Lenght = hangHoa.PackLenght,
					SoLuong = quantity
				};
					gioHang.Add(item);
				}
				else
				{
					item.SoLuong = 1;
				}
			    HttpContext.Session.Set(CART_KEY, gioHang);
			    return RedirectToAction("Index");
		}
            


        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.PackId == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

		

		
		[HttpPost]
		public IActionResult Checkout(CheckoutVM model, string payment = "Thanh toán VNPay")
		{
			if (ModelState.IsValid)
			{
				if (payment == "Thanh toán VNPay")
				{
					var vnPayModel = new VnPaymentRequestModel
					{
						Amount = Cart.Sum(p => p.ThanhTien),
						CreatedDate = DateTime.Now,
						Description = $"{model.HoTen} {model.DienThoai}",
						FullName = model.HoTen,
						OrderId = new Random().Next(1000, 100000)
					};

					
					
					return Redirect(_vnPayservice.CreatePaymentURL(HttpContext, vnPayModel));
				}

				

				
			}

			return View(Cart);
		}

		public IActionResult change()
		{
			DateOnly d = DateOnly.Parse(HttpContext.Session.GetString("RoleEndDate"));
			var existingUser = db.Users.Find(Int32.Parse(HttpContext.Session.GetString("id")));
			existingUser.Username = HttpContext.Session.GetString("Username");
			existingUser.Pass = HttpContext.Session.GetString("Pass");
			existingUser.UserRole = "1";
			existingUser.RoleStatus = "1";

			// Only update the Gmail property if a new value is provided
			string newGmail = HttpContext.Session.GetString("Gmail");
			if (!string.IsNullOrEmpty(newGmail))
			{
				existingUser.Gmail = newGmail;
			}

			// Update the RoleEndDate property
			if (d < DateOnly.FromDateTime(DateTime.Now))
			{
				existingUser.RoleEndDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(6);
			}
			else
			{
				existingUser.RoleEndDate = d.AddMonths(6);
			}
			HttpContext.Session.SetString("UserRole", existingUser.UserRole);
			HttpContext.Session.SetString("RoleEndDate", existingUser.RoleEndDate.ToString());
			// Save the changes to the database
			db.Entry(existingUser).State = EntityState.Modified;
			db.SaveChangesAsync();
			return View("~/Views/Shared/Success.cshtml");
		}


        public IActionResult PaymentSuccess()
        {
            change();
            var hoadon = new Bill
            {
                Total = Cart.Sum(p => p.ThanhTien),
            };
            db.Add(hoadon);
            db.SaveChangesAsync();
            HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
            return View("~/Views/Shared/Success.cshtml");
        }
        public IActionResult PaymentCallBack()
		{
			var response = _vnPayservice.PaymentExecute(Request.Query);

			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
				return View("~/Views/Shared/fail.cshtml");
			}


			// Lưu đơn hàng vô database

			TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }

	}
}

