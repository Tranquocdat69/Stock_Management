using System.Collections.Generic;
using System.Threading.Tasks;
using Stock_Management.Models;

namespace Stock_Management.Repository
{
    public interface IStockTableRepository
    {
        public Task<List<TbBangHienThi>> GetAllDataFromStockTable();
        public Task<bool> AddNewData(TbBangHienThi data);
        public Task<bool> UpdateData(string oldCode, TbBangHienThi data);
        public Task<bool> DeleteData(string code);
    }
}