﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Entities
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public string Role { get; set; }
                
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public string Image { get; set; }
    }
}
