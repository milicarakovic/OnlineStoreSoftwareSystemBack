using LibraryData.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interface
{
    public interface IlibraryUser
    {
         IEnumerable<User> GetUser(int UserID);

        User Authentificate(string username, string password);
        bool UpdateUser(int UserID, User user);
         bool AddUser(User user);
    }
}
