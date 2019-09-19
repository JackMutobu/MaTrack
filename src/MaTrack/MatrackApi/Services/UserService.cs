using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using MaTrack.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrackApi.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity Authenticate(string phone, string password)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetAll().SingleOrDefault(x => x.Phone == phone);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordHelper.Verify(password, user.Password))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<UserEntity> GetAll()
        {
            return _userRepository.GetAll();
        }

        public UserEntity GetById(int id)
        {
            return _userRepository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public async Task<UserEntity> Create(UserEntity user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_userRepository.GetAll().Any(x => x.Phone == user.Phone))
                throw new Exception("Phone number \"" + user.Phone + "\" is already taken");



            user.Password = PasswordHelper.Hash(password);

            await _userRepository.AddAsync(user);

            return user;
        }

        public async Task Update(UserEntity userParam, string password = null)
        {
            var user = await _userRepository.GetByIdAsync(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userParam.Phone != user.Phone)
            {
                // username has changed so check if the new username is already taken
                if (_userRepository.GetAll().Any(x => x.Phone == userParam.Phone))
                    throw new Exception("Username " + userParam.Phone + " is already taken");
            }

            // update user properties
            user.Phone = userParam.Phone;
            user.Phone = userParam.Phone;
            user.Phone = userParam.Phone;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                user.Password = PasswordHelper.Hash(password);
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }
    }
}
