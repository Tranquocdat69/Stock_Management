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
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Stock_Management.Hubs;
using Stock_Management.Models;

namespace Stock_Management.Controllers
{
    public class StockTableController : Controller
    {
        private readonly ChungKhoanContext _db;
        private readonly IHubContext<SignalrServer> _signalrHub;

        public StockTableController(ChungKhoanContext db, IHubContext<SignalrServer> signalrHub)
        {
            _db = db;
            _signalrHub = signalrHub;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDataTable()
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

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                            await _signalrHub.Clients.All.SendAsync("LoadDataTable");
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return View();
        }

        [Authorize]
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
                    await _signalrHub.Clients.All.SendAsync("LoadDataTable");
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(tbBangHienThi);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(string code)
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
        public async Task<IActionResult> Delete(string code)
        {
            using (var client = new HttpClient())
            {
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Delete;
                httpRequestMessage.RequestUri = new Uri("https://localhost:5001/stocktableapi/delete/" + code);

                HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    await _signalrHub.Clients.All.SendAsync("LoadDataTable");
                    return RedirectToAction(nameof(Index));
                }
            }
            return BadRequest("Không xóa được !!!");
        }
    }
}