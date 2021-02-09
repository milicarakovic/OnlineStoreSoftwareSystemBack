using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Library.Helpers;
using LibraryData.Entities;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private IlibraryUser _user;
        private readonly AppSettings _appSettings;

        public UserController(IlibraryUser user, IOptions<AppSettings> appSettings)
        {
            _user = user;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody]AuthenticateRequest request)
        {
            var user = _user.Authentificate(request.Username, request.Password);
            if (user is null) return NotFound();

            var tokenString = CreateToken(user);
            AuthenticateResponse response = CreateResponse(tokenString, user);
            return Ok(response);
        }

        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private AuthenticateResponse CreateResponse(string token, User user)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            response.Email = user.Email;
            response.Id = user.ID;
            response.Token = token;
            response.Username = user.Username;
            response.IsAdmin = user.IsAdmin;
            response.Name = user.Name;
            response.Password = user.Password;
            response.Surname = user.Surname;
            response.Phone = user.Phone;
            response.Image = user.Image;
            return response;
        }
        //STARE

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[AllowAnonymous]
        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task <ActionResult<User>> GetUser(int id)
        {
            var user = _user.GetUser(id);
            if (user == null) return NotFound();
            if (user.Count() == 0) return NoContent();

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public async Task <ActionResult<User>> PostUser([FromBody] User user)
        {
            if (_user.AddUser(user)) return Ok(user);
            return NotFound();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult PutUser(int id, [FromBody] User user)
        {
            if (_user.UpdateUser(id, user)) return Ok(user);
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
        }
    }
}
