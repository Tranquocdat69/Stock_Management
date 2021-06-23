using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock_Management.Models;
using Stock_Management.Repository;

namespace Stock_Management.DataManager
{
    public class StockTableRepository : IStockTableRepository
    {
        private readonly ChungKhoanContext _db;
        public StockTableRepository(ChungKhoanContext db)
        {
            this._db = db;
        }

        public async Task<bool> AddNewData(TbBangHienThi data)
        {
            int affectedRow = await _db.Database.ExecuteSqlRawAsync(
            "exec SP_THEM_BANG_HIEN_THI {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25}"
            , data.Ma,
            data.Tc,
            data.Tran,
            data.San,
            data.MuaG3,
            data.MuaKl3,
            data.MuaG2,
            data.MuaKl2,
            data.MuaG1,
            data.MuaKl1,
            data.KhopLenhGia,
            data.KhopLenhKl,
            data.TileTangGiam,
            data.BanG1,
            data.BanKl1,
            data.BanG2,
            data.BanKl2,
            data.BanG3,
            data.BanKl3,
            data.TongKl,
            data.MoCua,
            data.CaoNhat,
            data.ThapNhat,
            data.Nnmua,
            data.Nnban,
            data.Room);

            if (affectedRow > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteData(string code)
        {
            int affectedRow = await _db.Database.ExecuteSqlRawAsync("exec SP_XOA_BANG_HIEN_THI {0}", code);
            if (affectedRow > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<TbBangHienThi>> GetAllDataFromStockTable()
        {
            List<TbBangHienThi> tbBangHienThis = await _db.TbBangHienThis.FromSqlRaw("exec SP_GET_BANG_HIEN_THI").ToListAsync();
            return tbBangHienThis;
        }

        public async Task<bool> UpdateData(string oldCode, TbBangHienThi data)
        {
            int affectedRow = await _db.Database.ExecuteSqlRawAsync(
           "exec SP_CAP_NHAT_BANG_HIEN_THI {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}"
           , oldCode,
           data.Ma,
           data.Tc,
           data.Tran,
           data.San,
           data.MuaG3,
           data.MuaKl3,
           data.MuaG2,
           data.MuaKl2,
           data.MuaG1,
           data.MuaKl1,
           data.KhopLenhGia,
           data.KhopLenhKl,
           data.TileTangGiam,
           data.BanG1,
           data.BanKl1,
           data.BanG2,
           data.BanKl2,
           data.BanG3,
           data.BanKl3,
           data.TongKl,
           data.MoCua,
           data.CaoNhat,
           data.ThapNhat,
           data.Nnmua,
           data.Nnban,
           data.Room);

            if (affectedRow > 0)
            {
                return true;
            }
            return false;
        }
    }
}