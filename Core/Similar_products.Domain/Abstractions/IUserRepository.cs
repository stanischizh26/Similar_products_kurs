using Similar_products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Similar_products.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get(bool trackChanges, string? UserName);
        Task<User?> GetById(Guid id, bool trackChanges);
        Task Create(User entity);
        void Delete(User entity);
        void Update(User entity);
        Task SaveChanges();
        Task<IEnumerable<User>> GetPageAsync(int page, int pageSize, string? name);
        Task<int> CountAsync(string? name);
    }
}
