using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Management.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stock_Management.DataManager;
using System.Net.Http;
using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Stock_Management.Hubs;

namespace Stock_Management.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly ChungKhoanContext _db;
        private readonly IHubContext<SignalrServer> _signalrHub;
        public UserController(ChungKhoanContext db, IHubContext<SignalrServer> signalrHub)
        {
            _db = db;
            _signalrHub = signalrHub;
        }

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Display(Name = "Tên tài khoản (viết liền - không dấu)")]
            public string Name { set; get; }

            [Required]
            [EmailAddress]
            [Display(Name = "Địa chỉ Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Nhập lại mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
            public string ConfirmPassword { get; set; }

        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(InputModel Input)
        {
            if (Input.Name == null && Input.ConfirmPassword == null && Input.Email != null && Input.Password != null)
            {
                TbUser tbUser = _db.TbUsers.Where(u => u.Email == Input.Email).FirstOrDefault();
                if (tbUser != null)
                {
                    if (tbUser.Password == Input.Password)
                    {
                        var userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, tbUser.Name),
                            new Claim(ClaimTypes.Email, Input.Email),
                        };
                        var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        var userPrinciple = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrinciple);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Email hoặc mật khẩu không chính xác";
                    }
                }
                else
                {
                    ViewData["Error"] = "Email hoặc mật khẩu không chính xác";
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(InputModel Input)
        {
            if (ModelState.IsValid)
            {
                if (_db.TbUsers.ToList().Any(u => u.Email == Input.Email))
                {
                    ViewData["error"] = "Email đã tồn tại";
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        var httpRequestMessage = new HttpRequestMessage();
                        httpRequestMessage.Method = HttpMethod.Post;
                        httpRequestMessage.RequestUri = new Uri("https://localhost:5001/userapi/create_user");

                        string jsoncontent = JsonSerializer.Serialize(Input);
                        var httpContent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");

                        httpRequestMessage.Content = httpContent;

                        HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }
            }
            return View();
        }
    }
}