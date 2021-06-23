using System.Collections.Generic;
using System.Threading.Tasks;
using Stock_Management.Models;

namespace Stock_Management.Repository
{
    public interface IUserRepository
    {
        public Task<List<TbUser>> GetAllUsers();

        public Task<int> CreateUser(TbUser data);
    }
}