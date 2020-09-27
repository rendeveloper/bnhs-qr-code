using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using BnhsQrCode.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;

namespace BnhsQrCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.Users.ToList();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.Users.Single(x => x.Id == id);
                return Ok(user);
            }
            catch
            {
                return Ok(false);
            }
        }

        // POST: api/User/Scan
        [HttpPost]
        [Route("Scan")]
        public IActionResult Scan([FromBody] ScanDTO scan)
        {
            try
            {
                var user = _userService.GetByTeacherId(scan.TeacherId);
                return Ok(user);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDto)
        {
            try
            {
                _userService.BeginTransaction();

                var userModel = new User
                {
                    TeacherId = userDto.TeacherId,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    MiddleName = userDto.MiddleName,
                    DateOfBirth = userDto.DateOfBirth,
                    Address = userDto.Address,
                    HealthStatus = userDto.HealthStatus,
                    Department = userDto.Department,
                    Role = userDto.Role,
                    Image = userDto.Image
                };

                await _userService.Save(userModel);
                await _userService.Commit();
            }
            catch
            {
                // log exception here
                await _userService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userService.CloseTransaction();
            }
            return Ok(true);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO userDto)
        {
            var userModel = await _userService.Users.SingleAsync(x => x.Id == userDto.Id);
            try
            {
                _userService.BeginTransaction();

                userModel.TeacherId = userDto.TeacherId;
                userModel.FirstName = userDto.FirstName;
                userModel.LastName = userDto.LastName;
                userModel.MiddleName = userDto.MiddleName;
                userModel.DateOfBirth = userDto.DateOfBirth;
                userModel.Address = userDto.Address;
                userModel.HealthStatus = userDto.HealthStatus;
                userModel.Department = userDto.Department;
                userModel.Role = userDto.Role;
                userModel.Image = userDto.Image.Length == 0 ? userModel.Image : userDto.Image;

                await _userService.Save(userModel);
                await _userService.Commit();
            }
            catch
            {
                // log exception here
                await _userService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userService.CloseTransaction();
            }
            return Ok(userModel);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _userService.BeginTransaction();

                var user = _userService.Users.Single(x => x.Id == id);

                await _userService.Delete(user);
                await _userService.Commit();
            }
            catch
            {
                // log exception here
                await _userService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userService.CloseTransaction();
            }
            return Ok(true);
        }
    }
}
