using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_Management.Models;

namespace Stock_Management.Controllers
{
    public class StockTableController : Controller
    {
        private readonly ChungKhoanContext _db;
        public StockTableController(ChungKhoanContext db)
        {
            _db = db;
        }

        [Authorize]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            string stringResult = "";
            List<TbBangHienThi> result = new List<TbBangHienThi>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/stocktableapi/");
                HttpResponseMessage response = await client.GetAsync("get_all_data");
                response.EnsureSuccessStatusCode();
                stringResult = await response.Content.ReadAsStringAsync();
            }
            result = JsonSerializer.Deserialize<List<TbBangHienThi>>(stringResult, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            ViewData["data"] = result;

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TbBangHienThi data)
        {
            if (ModelState.IsValid)
            {
                List<TbBangHienThi> tbBangHienThis = await _db.TbBangHienThis.ToListAsync();
                if (tbBangHienThis.Any(b => b.Ma == data.Ma.Trim()))
                {
                    ViewData["error"] = "Mã đã tồn tại";
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        var httpRequestMessage = new HttpRequestMessage();
                        httpRequestMessage.Method = HttpMethod.Post;
                        httpRequestMessage.RequestUri = new Uri("https://localhost:5001/stocktableapi/add");
                        string jsoncontent = JsonSerializer.Serialize(data);
                        var httpContent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
                        httpRequestMessage.Content = httpContent;

                        HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var tbBangHienThi = await _db.TbBangHienThis.FindAsync(code);
            if (tbBangHienThi == null)
            {
                return NotFound();
            }
            return View(tbBangHienThi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string code, TbBangHienThi data)
        {
            var tbBangHienThi = await _db.TbBangHienThis.FindAsync(code);

            using (var client = new HttpClient())
            {
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Put;
                httpRequestMessage.RequestUri = new Uri("https://localhost:5001/stocktableapi/update/" + code);
                string jsoncontent = JsonSerializer.Serialize(data);
                var httpContent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;

                HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(tbBangHienThi);
        }
    }
}