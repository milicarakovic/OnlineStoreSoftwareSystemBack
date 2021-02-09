using LibraryData;
using LibraryData.Interface;
using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nest;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace LibraryService.Service
{
    public class LibraryUser : IlibraryUser
    {
        private User admin =
            new User { ID = 1000, Email = "milica@gmail.com", Name = "Milica", Surname = "Rakovic", Password = "milica", Username = "milica", Phone = "394729837" };
        

        private ApplicationDbContext _db;
        public LibraryUser(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool IsThereAUser(string username)
        {
            foreach(User u in _db.User)
            {
                if (u.Username == username) return true;
            }
            return false;
        }
        public bool AddUser(User user)
        {
            try
            {
                if (IsThereAUser(user.Username) == true) return false;
                _db.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public User Authentificate(string username, string password)
        {
            try
            {
                var user = _db.User.SingleOrDefault((u) => u.Username.Equals(username) && u.Password == password);
                if (user is null) return null;


                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<User> GetUser(int UserID)
        {
            try
            {
                return _db.User.Where(u => u.ID == UserID);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool UpdateUser(int UserID, User updatedUser)
        {          
            try
            {
                var user = _db.User.SingleOrDefault(u => u.ID == UserID);
                if (user == null) return false;

                _db.Entry(user).CurrentValues.SetValues(updatedUser);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
