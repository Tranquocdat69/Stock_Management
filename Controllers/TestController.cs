using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_Management.Models;

namespace Stock_Management.Controllers
{
    public class TestController : Controller
    {
        private readonly ChungKhoanContext _context;

        public TestController(ChungKhoanContext context)
        {
            _context = context;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbBangHienThis.ToListAsync());
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBangHienThi = await _context.TbBangHienThis
                .FirstOrDefaultAsync(m => m.Ma == id);
            if (tbBangHienThi == null)
            {
                return NotFound();
            }

            return View(tbBangHienThi);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ma,Tc,Tran,San,MuaG3,MuaKl3,MuaG2,MuaKl2,MuaG1,MuaKl1,KhopLenhGia,KhopLenhKl,TileTangGiam,BanG1,BanKl1,BanG2,BanKl2,BanG3,BanKl3,TongKl,MoCua,CaoNhat,ThapNhat,Nnmua,Nnban,Room")] TbBangHienThi tbBangHienThi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbBangHienThi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbBangHienThi);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBangHienThi = await _context.TbBangHienThis.FindAsync(id);
            if (tbBangHienThi == null)
            {
                return NotFound();
            }
            return View(tbBangHienThi);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ma,Tc,Tran,San,MuaG3,MuaKl3,MuaG2,MuaKl2,MuaG1,MuaKl1,KhopLenhGia,KhopLenhKl,TileTangGiam,BanG1,BanKl1,BanG2,BanKl2,BanG3,BanKl3,TongKl,MoCua,CaoNhat,ThapNhat,Nnmua,Nnban,Room")] TbBangHienThi tbBangHienThi)
        {
            if (id != tbBangHienThi.Ma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbBangHienThi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbBangHienThiExists(tbBangHienThi.Ma))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbBangHienThi);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBangHienThi = await _context.TbBangHienThis
                .FirstOrDefaultAsync(m => m.Ma == id);
            if (tbBangHienThi == null)
            {
                return NotFound();
            }

            return View(tbBangHienThi);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tbBangHienThi = await _context.TbBangHienThis.FindAsync(id);
            _context.TbBangHienThis.Remove(tbBangHienThi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbBangHienThiExists(string id)
        {
            return _context.TbBangHienThis.Any(e => e.Ma == id);
        }
    }
}
