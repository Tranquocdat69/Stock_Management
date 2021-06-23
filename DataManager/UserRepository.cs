using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock_Management.Models;
using Stock_Management.Repository;

namespace Stock_Management.DataManager
{
    public class UserRepository : IUserRepository
    {
        private readonly ChungKhoanContext _db;
        public UserRepository(ChungKhoanContext db)
        {
            this._db = db;
        }

        public async Task<int> CreateUser(TbUser data)
        {
            int affectedRow = await _db.Database.ExecuteSqlRawAsync("exec SP_Insert_User {0},{1},{2},{3}", data.Name, data.Email, data.Password, "");
            return affectedRow;
        }

        public async Task<List<TbUser>> GetAllUsers()
        {
            var result = await _db.TbUsers.FromSqlRaw("exec SP_Get_All_User").ToListAsync();
            return result;
        }
    }
}