using System.Collections.Generic;
using System.Threading.Tasks;
using MaTrack.Core.Entities;

namespace MatrackApi.Services
{
    public interface IUserService 
    {
        UserEntity Authenticate(string phone, string password);
        Task<UserEntity> Create(UserEntity user, string password);
        Task Delete(int id);
        IEnumerable<UserEntity> GetAll();
        UserEntity GetById(int id);
        Task Update(UserEntity userParam, string password = null);
    }
}